using System;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting;

/// <summary>
/// Provides a set of static methods for <see cref="IHost"/> testing that bootstraps an existing .NET application entry point.
/// </summary>
public static class ApplicationTestFactory
{
    /// <summary>
    /// Creates and returns an <see cref="IHostTest"/> implementation.
    /// </summary>
    /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
    /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
    /// <param name="hostFixture">An optional <see cref="IApplicationFixture{TEntryPoint}"/> implementation to use instead of the default <see cref="ManagedApplicationFixture{TEntryPoint}"/> instance.</param>
    /// <returns>An instance of an <see cref="IHostTest"/> implementation.</returns>
    /// <remarks>
    /// When <paramref name="hostFixture"/> is omitted, the factory uses <see cref="ManagedApplicationFixture{TEntryPoint}"/> so the application entry point owns deferred host startup. Pass another <see cref="IApplicationFixture{TEntryPoint}"/> implementation when the test requires a different lifecycle.
    /// </remarks>
    public static IHostTest Create<TEntryPoint>(Action<IHostBuilder> hostSetup = null, IApplicationFixture<TEntryPoint> hostFixture = null) where TEntryPoint : class
    {
        return new Internal.ApplicationTest<TEntryPoint>(hostSetup, hostFixture ?? new ManagedApplicationFixture<TEntryPoint>());
    }
}
