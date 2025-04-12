using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Provides a set of static methods for <see cref="IHost"/> unit testing.
    /// </summary>
    public static class HostTestFactory
    {
        /// <summary>
        /// Creates and returns an <see cref="IHostTest" /> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="hostFixture">An optional <see cref="IGenericHostFixture"/> implementation to use instead of the default <see cref="ManagedHostFixture"/> instance.</param>
        /// <returns>An instance of an <see cref="IHostTest" /> implementation.</returns>
        public static IHostTest Create(Action<IServiceCollection> serviceSetup = null, Action<IHostBuilder> hostSetup = null, IGenericHostFixture hostFixture = null)
        {
            return new Internal.HostTest(serviceSetup, hostSetup, hostFixture ?? new ManagedHostFixture());
        }

        /// <summary>
        /// Creates and returns an <see cref="IHostTest" /> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="hostFixture">An optional <see cref="IGenericHostFixture"/> implementation to use instead of the default <see cref="ManagedHostFixture"/> instance.</param>
        /// <returns>An instance of an <see cref="IHostTest" /> implementation.</returns>
        public static IHostTest CreateWithHostBuilderContext(Action<HostBuilderContext, IServiceCollection> serviceSetup = null, Action<IHostBuilder> hostSetup = null, IGenericHostFixture hostFixture = null)
        {
            return new Internal.HostTest(serviceSetup, hostSetup, hostFixture ?? new ManagedHostFixture());
        }
    }
}
