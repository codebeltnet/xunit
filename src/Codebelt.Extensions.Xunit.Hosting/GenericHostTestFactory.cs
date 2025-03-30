using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Provides a set of static methods for <see cref="IHost"/> unit testing.
    /// </summary>
    public static class GenericHostTestFactory
    {
        /// <summary>
        /// Creates and returns an <see cref="IGenericHostTest" /> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <returns>An instance of an <see cref="IGenericHostTest" /> implementation.</returns>
        [Obsolete("This method is obsolete and will be removed in a future version. Use Create(Action<IServiceCollection>, Action<IHostBuilder>, IHostFixture) instead.")]
        public static IGenericHostTest Create(Action<IServiceCollection> serviceSetup = null, Action<IHostBuilder> hostSetup = null)
        {
            return Create(serviceSetup, hostSetup, null);
        }

        /// <summary>
        /// Creates and returns an <see cref="IGenericHostTest" /> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="hostFixture">An optional <see cref="IHostFixture"/> implementation to use instead of the default <see cref="HostFixture"/> instance.</param>
        /// <returns>An instance of an <see cref="IGenericHostTest" /> implementation.</returns>
        public static IGenericHostTest Create(Action<IServiceCollection> serviceSetup = null, Action<IHostBuilder> hostSetup = null, IHostFixture hostFixture = null)
        {
            return new GenericHostTest(serviceSetup, hostSetup, hostFixture ?? new HostFixture());
        }

        /// <summary>
        /// Creates and returns an <see cref="IGenericHostTest" /> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <returns>An instance of an <see cref="IGenericHostTest" /> implementation.</returns>
        [Obsolete("This method is obsolete and will be removed in a future version. Use CreateWithHostBuilderContext(Action<HostBuilderContext, IServiceCollection>, Action<IHostBuilder>, IHostFixture) instead.")]
        public static IGenericHostTest CreateWithHostBuilderContext(Action<HostBuilderContext, IServiceCollection> serviceSetup = null, Action<IHostBuilder> hostSetup = null)
        {
            return CreateWithHostBuilderContext(serviceSetup, hostSetup, null);
        }

        /// <summary>
        /// Creates and returns an <see cref="IGenericHostTest" /> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="hostFixture">An optional <see cref="IHostFixture"/> implementation to use instead of the default <see cref="HostFixture"/> instance.</param>
        /// <returns>An instance of an <see cref="IGenericHostTest" /> implementation.</returns>
        public static IGenericHostTest CreateWithHostBuilderContext(Action<HostBuilderContext, IServiceCollection> serviceSetup = null, Action<IHostBuilder> hostSetup = null, IHostFixture hostFixture = null)
        {
            return new GenericHostTest(serviceSetup, hostSetup, hostFixture ?? new HostFixture());
        }
    }
}
