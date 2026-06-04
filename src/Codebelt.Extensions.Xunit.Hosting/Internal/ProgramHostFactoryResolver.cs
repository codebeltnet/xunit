using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.Internal;

// Adapted from Microsoft.Extensions.Hosting.HostFactoryResolver.
// Licensed to the .NET Foundation under one or more agreements under the MIT license.
internal static class ProgramHostFactoryResolver
{
    private const BindingFlags DeclaredOnlyLookup = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
    private static readonly TimeSpan DefaultWaitTimeout = Debugger.IsAttached ? Timeout.InfiniteTimeSpan : TimeSpan.FromMinutes(5);

    public static Func<string[], IHostBuilder> ResolveHostBuilderFactory(Assembly assembly)
    {
        return ResolveFactory<IHostBuilder>(assembly, "CreateHostBuilder");
    }

    public static Func<string[], object> ResolveHostFactory(Assembly assembly, bool stopApplication, Action<object> configureHostBuilder, Action<Exception> entryPointCompleted)
    {
        if (assembly.EntryPoint == null) { return null; }

        try
        {
            var hostingAssembly = Assembly.Load("Microsoft.Extensions.Hosting");
            if (hostingAssembly.GetName().Version is Version version && version.Major < 6) { return null; }
        }
        catch
        {
            return null;
        }

        return args => new HostingListener(args, assembly.EntryPoint, DefaultWaitTimeout, stopApplication, configureHostBuilder, entryPointCompleted).CreateHost();
    }

    private static Func<string[], T> ResolveFactory<T>(Assembly assembly, string name)
    {
        var programType = assembly.EntryPoint?.DeclaringType;
        if (programType == null) { return null; }

        var factory = programType.GetMethod(name, DeclaredOnlyLookup);
        if (!IsFactory<T>(factory)) { return null; }

        return args => (T)factory.Invoke(null, new object[] { args });
    }

    private static bool IsFactory<T>(MethodInfo factory)
    {
        return factory != null &&
               typeof(T).IsAssignableFrom(factory.ReturnType) &&
               factory.GetParameters().Length == 1 &&
               typeof(string[]).Equals(factory.GetParameters()[0].ParameterType);
    }

    private sealed class HostingListener : IObserver<DiagnosticListener>, IObserver<KeyValuePair<string, object>>
    {
        private static readonly AsyncLocal<HostingListener> CurrentListener = new();

        private readonly string[] _args;
        private readonly Action<object> _configure;
        private readonly Action<Exception> _entryPointCompleted;
        private readonly MethodInfo _entryPoint;
        private readonly TaskCompletionSource<object> _host = new(TaskCreationOptions.RunContinuationsAsynchronously);
        private readonly bool _stopApplication;
        private readonly TimeSpan _waitTimeout;
        private IDisposable _disposable;

        public HostingListener(string[] args, MethodInfo entryPoint, TimeSpan waitTimeout, bool stopApplication, Action<object> configure, Action<Exception> entryPointCompleted)
        {
            _args = args;
            _entryPoint = entryPoint;
            _waitTimeout = waitTimeout;
            _stopApplication = stopApplication;
            _configure = configure;
            _entryPointCompleted = entryPointCompleted;
        }

        public object CreateHost()
        {
            using var subscription = DiagnosticListener.AllListeners.Subscribe(this);
            var thread = new Thread(InvokeEntryPoint)
            {
                IsBackground = true
            };

            thread.Start();

            if (!_host.Task.Wait(_waitTimeout))
            {
                throw new InvalidOperationException($"Timed out waiting for the entry point to build the IHost after {DefaultWaitTimeout}.");
            }

            return _host.Task.GetAwaiter().GetResult();
        }

        public void OnCompleted()
        {
            _disposable?.Dispose();
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(DiagnosticListener value)
        {
            if (CurrentListener.Value != this) { return; }
            if (value.Name == "Microsoft.Extensions.Hosting")
            {
                _disposable = value.Subscribe(this);
            }
        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            if (CurrentListener.Value != this) { return; }

            if (value.Key == "HostBuilding")
            {
                _configure?.Invoke(value.Value);
            }

            if (value.Key == "HostBuilt")
            {
                _host.TrySetResult(value.Value);
                if (_stopApplication)
                {
                    throw new HostAbortedException();
                }
            }
        }

        private void InvokeEntryPoint()
        {
            Exception exception = null;
            try
            {
                CurrentListener.Value = this;
                var parameters = _entryPoint.GetParameters();
                var result = parameters.Length == 0
                    ? _entryPoint.Invoke(null, Array.Empty<object>())
                    : _entryPoint.Invoke(null, new object[] { _args });

                if (result is Task task)
                {
                    task.GetAwaiter().GetResult();
                }

                _host.TrySetException(new InvalidOperationException("The entry point exited without ever building an IHost."));
            }
            catch (TargetInvocationException ex) when (ex.InnerException?.GetType().Name == nameof(HostAbortedException))
            {
            }
            catch (TargetInvocationException ex)
            {
                exception = ex.InnerException ?? ex;
                _host.TrySetException(exception);
            }
            catch (Exception ex)
            {
                exception = ex;
                _host.TrySetException(ex);
            }
            finally
            {
                CurrentListener.Value = null;
                _entryPointCompleted?.Invoke(exception);
            }
        }

        private sealed class HostAbortedException : Exception
        {
        }
    }
}
