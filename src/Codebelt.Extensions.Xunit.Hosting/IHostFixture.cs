using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
    public partial interface IHostFixture
    {
        /// <summary>
        /// Gets or sets the delegate that adds configuration and environment information to a <see cref="HostTest{T}"/>.
        /// </summary>
        /// <value>The delegate that adds configuration and environment information to a <see cref="HostTest{T}"/>.</value>
        Action<IConfiguration, IHostEnvironment> ConfigureCallback { get; set; }
    }

    /// <summary>
    /// Provides a way to use Microsoft Dependency Injection in unit tests.
    /// </summary>
    /// <seealso cref="IServiceTest" />
    /// <seealso cref="IHostTest" />
    /// <seealso cref="IConfigurationTest" />
    /// <seealso cref="IHostingEnvironmentTest" />
    /// <seealso cref="IDisposable" />
    /// <seealso cref="IAsyncDisposable" />
    public partial interface IHostFixture : IServiceTest, IHostTest, IConfigurationTest, IHostingEnvironmentTest, IDisposable, IAsyncDisposable
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
