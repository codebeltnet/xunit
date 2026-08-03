using System;
using Codebelt.Extensions.Xunit.Hosting.Internal;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting;

/// <summary>
/// Provides factory methods for creating application hosts from an entry point assembly.
/// </summary>
public static class ApplicationHostFactory
{
    /// <summary>
    /// Creates, configures and builds an <see cref="IHost"/> from the assembly containing <typeparamref name="TEntryPoint"/>.
    /// </summary>
    /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
    /// <param name="configureHost">The delegate that provides a way to override the <see cref="IHostBuilder"/> before the application is built.</param>
    /// <returns>A built <see cref="IHost"/> instance.</returns>
    /// <remarks>
    /// For compatibility, applications that expose <c>CreateHostBuilder(string[])</c> are built through that factory. Applications that do not expose the legacy factory use the deferred entry-point path.
    /// The legacy path and its wrapper behavior are retained for the current minor release; only the managed application fixtures opt into entrypoint-owned deferred startup. The legacy path should be removed or changed in the next major release.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// The entry point assembly does not expose a supported application host.
    /// </exception>
    public static IHost Create<TEntryPoint>(Action<IHostBuilder> configureHost) where TEntryPoint : class
    {
        return Create<TEntryPoint>(configureHost, true);
    }

    /// <summary>
    /// Creates, configures and builds an <see cref="IHost"/> from the assembly containing <typeparamref name="TEntryPoint"/>.
    /// </summary>
    /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
    /// <param name="configureHost">The delegate that provides a way to override the <see cref="IHostBuilder"/> before the application is built.</param>
    /// <param name="stopApplication">A value indicating whether the entry point should be stopped after the host is built.</param>
    /// <returns>A built <see cref="IHost"/> instance.</returns>
    /// <remarks>
    /// For compatibility, applications that expose <c>CreateHostBuilder(string[])</c> are built through that factory and the <paramref name="stopApplication"/> value is ignored for that path.
    /// The legacy path and its wrapper behavior are retained for the current minor release; only the managed application fixtures opt into entrypoint-owned deferred startup. The legacy path should be removed or changed in the next major release.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// The entry point assembly does not expose a supported application host.
    /// </exception>
    public static IHost Create<TEntryPoint>(Action<IHostBuilder> configureHost, bool stopApplication) where TEntryPoint : class
    {
        var assembly = typeof(TEntryPoint).Assembly;

        // Compatibility behavior for the current minor release: preserve CreateHostBuilder-first resolution.
        // Major release: remove this branch and make the deferred entry-point path the default.
        var hostBuilder = ProgramHostFactoryResolver.ResolveHostBuilderFactory(assembly)?.Invoke(Array.Empty<string>());
        if (hostBuilder != null)
        {
            hostBuilder.UseEnvironment(Environments.Development);
            return HostBuilderFactory.Build(hostBuilder, configureHost);
        }

        return DeferredHostFactory.Create(assembly, configureHost, stopApplication, false);
    }
}
