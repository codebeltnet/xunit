using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Provides a set of static methods for <see cref="IHost"/> unit testing (minimal style).
    /// </summary>
    public static class MinimalHostTestFactory
    {
        /// <summary>
        /// Creates and returns an <see cref="IHostTest" /> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="hostFixture">An optional <see cref="IMinimalHostFixture"/> implementation to use instead of the default <see cref="ManagedMinimalHostFixture"/> instance.</param>
        /// <returns>An instance of an <see cref="IHostTest" /> implementation.</returns>
        public static IHostTest Create(Action<IServiceCollection> serviceSetup = null, Action<IHostApplicationBuilder> hostSetup = null, IMinimalHostFixture hostFixture = null)
        {
            return new Internal.MinimalHostTest(serviceSetup, hostSetup, hostFixture ?? new ManagedMinimalHostFixture());
        }

        /// <summary>
        /// Creates and returns an <see cref="IHostTest" /> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="hostFixture">An optional <see cref="IMinimalHostFixture"/> implementation to use instead of the default <see cref="ManagedMinimalHostFixture"/> instance.</param>
        /// <returns>An instance of an <see cref="IHostTest" /> implementation.</returns>
        public static IHostTest CreateWithHostBuilderContext(Action<HostBuilderContext, IServiceCollection> serviceSetup = null, Action<IHostApplicationBuilder> hostSetup = null, IMinimalHostFixture hostFixture = null)
        {
            return new Internal.MinimalHostTest(serviceSetup, hostSetup, hostFixture ?? new ManagedMinimalHostFixture());
        }
    }
}
