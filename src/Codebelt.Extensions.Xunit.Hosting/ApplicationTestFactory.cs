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
    /// <param name="hostFixture">An optional <see cref="IApplicationFixture{TEntryPoint}"/> implementation to use instead of the default <see cref="BlockingManagedApplicationFixture{TEntryPoint}"/> instance.</param>
    /// <returns>An instance of an <see cref="IHostTest"/> implementation.</returns>
    /// <remarks>
    /// Passing a <see cref="ManagedApplicationFixture{TEntryPoint}"/> opts this call into entrypoint-owned deferred startup. Omitting <paramref name="hostFixture"/> preserves the blocking compatibility path for the current minor release; that default should be removed or changed in the next major release.
    /// </remarks>
    public static IHostTest Create<TEntryPoint>(Action<IHostBuilder> hostSetup = null, IApplicationFixture<TEntryPoint> hostFixture = null) where TEntryPoint : class
    {
        // Minor-release compatibility: keep the historical blocking default while allowing callers to opt in by passing ManagedApplicationFixture<TEntryPoint>.
        // Major release: remove or change this default when the compatibility fixture is retired. Hint: ManagedApplicationFixture
        return new Internal.ApplicationTest<TEntryPoint>(hostSetup, hostFixture ?? new BlockingManagedApplicationFixture<TEntryPoint>());
    }
}
