using System;
using System.Collections.Generic;
using Codebelt.Extensions.Xunit.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting;

/// <summary>
/// Provides factory methods for creating application hosts from an entry point assembly.
/// </summary>
public static class ApplicationHostFactory
{
    /// <summary>
    /// Creates, configures, builds and starts an <see cref="IHost"/> from the assembly containing <typeparamref name="TEntryPoint"/>.
    /// </summary>
    /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
    /// <param name="configureHost">The delegate that provides a way to override the <see cref="IHostBuilder"/> before the application is built.</param>
    /// <returns>A started <see cref="IHost"/> instance.</returns>
    /// <exception cref="InvalidOperationException">
    /// The entry point assembly does not expose a supported application host.
    /// </exception>
    public static IHost Create<TEntryPoint>(Action<IHostBuilder> configureHost) where TEntryPoint : class
    {
        return Create<TEntryPoint>(configureHost, true);
    }

    /// <summary>
    /// Creates, configures, builds and starts an <see cref="IHost"/> from the assembly containing <typeparamref name="TEntryPoint"/>.
    /// </summary>
    /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
    /// <param name="configureHost">The delegate that provides a way to override the <see cref="IHostBuilder"/> before the application is built.</param>
    /// <param name="stopApplication">A value indicating whether the entry point should be stopped after the host is built.</param>
    /// <returns>A started <see cref="IHost"/> instance.</returns>
    /// <exception cref="InvalidOperationException">
    /// The entry point assembly does not expose a supported application host.
    /// </exception>
    public static IHost Create<TEntryPoint>(Action<IHostBuilder> configureHost, bool stopApplication) where TEntryPoint : class
    {
        var assembly = typeof(TEntryPoint).Assembly;
        var hostBuilder = ProgramHostFactoryResolver.ResolveHostBuilderFactory(assembly)?.Invoke(Array.Empty<string>());

        if (hostBuilder != null)
        {
            hostBuilder.UseEnvironment(Environments.Development);
            return BuildHost(hostBuilder, configureHost);
        }

        var deferredHostBuilder = new DeferredHostBuilder();

        deferredHostBuilder.UseEnvironment(Environments.Development);
        deferredHostBuilder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                [HostDefaults.ApplicationKey] = assembly.GetName().Name
            });
        });

        var hostFactory = ProgramHostFactoryResolver.ResolveHostFactory(assembly, stopApplication, deferredHostBuilder.ConfigureHostBuilder, deferredHostBuilder.EntryPointCompleted);
        if (hostFactory == null)
        {
            throw new InvalidOperationException($"The entry point assembly '{assembly.GetName().Name}' does not expose a supported application host.");
        }

        deferredHostBuilder.SetHostFactory(hostFactory);
        return BuildHost(deferredHostBuilder, configureHost);
    }

    private static IHost BuildHost(IHostBuilder hostBuilder, Action<IHostBuilder> configureHost)
    {
        configureHost?.Invoke(hostBuilder);

#if NET9_0_OR_GREATER
        hostBuilder.UseDefaultServiceProvider(o =>
        {
            o.ValidateOnBuild = true;
            o.ValidateScopes = true;
        });
#endif

        var host = hostBuilder.Build();
        if (hostBuilder is IDisposable disposable)
        {
            disposable.Dispose();
        }

        return host;
    }
}
