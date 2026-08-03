using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.Internal;

internal sealed class DeferredHostBuilder : IHostBuilder, IDisposable
{
    private readonly ConfigurationManager _hostConfiguration = new();
    private readonly TaskCompletionSource<object> _hostStarted = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private readonly bool _entrypointOwned;
    private Action<IHostBuilder> _configure;
    private Func<string[], object> _hostFactory;

    public DeferredHostBuilder(bool entrypointOwned)
    {
        _entrypointOwned = entrypointOwned;
        _configure = builder =>
        {
            foreach (var pair in Properties)
            {
                builder.Properties[pair.Key] = pair.Value;
            }
        };
    }

    public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();

    public IHost Build()
    {
        var args = new List<string>();

        foreach (var pair in _hostConfiguration.AsEnumerable())
        {
            args.Add($"--{pair.Key}={pair.Value}");
        }

        var capture = (ProgramHostFactoryResolver.HostCapture)_hostFactory(args.ToArray());
        try
        {
            var host = capture.Host;
            // Preserve the legacy wrapper for ApplicationHostFactory fallback callers. Only managed application fixtures opt into the marker that HostTest uses for lazy startup.
            var deferredHost = _entrypointOwned
                ? new EntrypointOwnedDeferredHost(host, _hostStarted, capture)
                : new DeferredHost(host, _hostStarted, capture);

            if (!_entrypointOwned)
            {
                capture.Release();
            }

            return deferredHost;
        }
        catch
        {
            capture.Release();
            throw;
        }
    }

    public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
    {
        _configure += builder => builder.ConfigureAppConfiguration(configureDelegate);
        return this;
    }

    public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
    {
        _configure += builder => builder.ConfigureContainer(configureDelegate);
        return this;
    }

    public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
    {
        configureDelegate(_hostConfiguration);
        return this;
    }

    public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
    {
        _configure += builder => builder.ConfigureServices(configureDelegate);
        return this;
    }

    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory) where TContainerBuilder : notnull
    {
        _configure += builder => builder.UseServiceProviderFactory(factory);
        return this;
    }

    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory) where TContainerBuilder : notnull
    {
        _configure += builder => builder.UseServiceProviderFactory(factory);
        return this;
    }

    public void ConfigureHostBuilder(object hostBuilder)
    {
        _configure((IHostBuilder)hostBuilder);
    }

    public void EntryPointCompleted(Exception exception)
    {
        if (exception == null)
        {
            _hostStarted.TrySetResult(null);
            return;
        }

        _hostStarted.TrySetException(exception);
    }

    public void SetHostFactory(Func<string[], object> hostFactory)
    {
        _hostFactory = hostFactory;
    }

    public void Dispose()
    {
        _hostConfiguration.Dispose();
    }

    private class DeferredHost : IHost, IAsyncDisposable
    {
        private readonly IHost _host;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly ProgramHostFactoryResolver.HostCapture _capture;
        private readonly TaskCompletionSource<object> _hostStarted;

        public DeferredHost(IHost host, TaskCompletionSource<object> hostStarted, ProgramHostFactoryResolver.HostCapture capture)
        {
            _host = host;
            _applicationLifetime = capture.ApplicationLifetime;
            _capture = capture;
            _hostStarted = hostStarted;
        }

        public IServiceProvider Services => _host.Services;

        public void Dispose()
        {
            _host.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (_host is IAsyncDisposable disposable)
            {
                await disposable.DisposeAsync().ConfigureAwait(false);
                return;
            }

            Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            if (_hostStarted.Task.IsCompleted)
            {
                await _hostStarted.Task.ConfigureAwait(false);
                return;
            }

            using var registration = cancellationToken.Register(() => _hostStarted.TrySetCanceled());
            using var startedRegistration = _applicationLifetime.ApplicationStarted.Register(() => _hostStarted.TrySetResult(null));

            await _hostStarted.Task.ConfigureAwait(false);
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            return _host.StopAsync(cancellationToken);
        }

        public void ReleaseEntrypoint()
        {
            _capture.Release();
        }
    }

    private sealed class EntrypointOwnedDeferredHost : DeferredHost, IDeferredHost
    {
        public EntrypointOwnedDeferredHost(IHost host, TaskCompletionSource<object> hostStarted, ProgramHostFactoryResolver.HostCapture capture) : base(host, hostStarted, capture)
        {
        }
    }
}
