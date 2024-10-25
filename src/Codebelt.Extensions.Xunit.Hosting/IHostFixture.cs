using System;
#if NETSTANDARD2_0_OR_GREATER
using System.Threading.Tasks;
#endif
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
#if NETSTANDARD2_0_OR_GREATER
    public partial interface IHostFixture
    {
        /// <summary>
        /// Gets or sets the delegate that adds configuration and environment information to a <see cref="HostTest{T}"/>.
        /// </summary>
        /// <value>The delegate that adds configuration and environment information to a <see cref="HostTest{T}"/>.</value>
        Action<IConfiguration, IHostingEnvironment> ConfigureCallback { get; set; }

        /// <summary>
        /// Asynchronously releases the resources used by the <see cref="Test"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous dispose operation.</returns>
        ValueTask DisposeAsync();
    }
#else
    public partial interface IHostFixture : IAsyncDisposable
    {
        /// <summary>
        /// Gets or sets the delegate that adds configuration and environment information to a <see cref="HostTest{T}"/>.
        /// </summary>
        /// <value>The delegate that adds configuration and environment information to a <see cref="HostTest{T}"/>.</value>
        Action<IConfiguration, IHostEnvironment> ConfigureCallback { get; set; }
    }
#endif

    /// <summary>
    /// Provides a way to use Microsoft Dependency Injection in unit tests.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public partial interface IHostFixture : IServiceTest, IHostTest, IConfigurationTest, IHostingEnvironmentTest, IDisposable
    {
        /// <summary>
        /// Gets or sets the delegate that adds services to the container.
        /// </summary>
        /// <value>The delegate that adds services to the container.</value>
        Action<IServiceCollection> ConfigureServicesCallback { get; set; }

        /// <summary>
        /// Gets or sets the delegate that provides a way to override the <see cref="IHostBuilder"/> defaults set up by <see cref="ConfigureHost"/>.
        /// </summary>
        /// <value>The delegate that provides a way to override the <see cref="IHostBuilder"/>.</value>
        Action<IHostBuilder> ConfigureHostCallback { get; set; }

        /// <summary>
        /// Creates and configures the <see cref="IHost"/> of this <see cref="IHostFixture"/>.
        /// </summary>
        /// <param name="hostTest">The object that inherits from <see cref="HostTest{T}"/>.</param>
        /// <remarks><paramref name="hostTest"/> was added to support those cases where the caller is required in the host configuration.</remarks>
        void ConfigureHost(Test hostTest);
    }
}
