using Microsoft.Extensions.Hosting;
using System;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Provides a way to use Microsoft Dependency Injection in unit tests (minimal style).
    /// </summary>
    /// <seealso cref="IHostFixture" />
    /// <seealso cref="IConfigurationTest" />
    /// <seealso cref="IEnvironmentTest" />
    public interface IMinimalHostFixture : IHostFixture
    {
        /// <summary>
        /// Gets or sets the delegate that provides a way to override the <see cref="IHostApplicationBuilder"/> defaults set up by <see cref="ConfigureHost"/>.
        /// </summary>
        /// <value>The delegate that provides a way to override the <see cref="IHostApplicationBuilder"/>.</value>
        Action<IHostApplicationBuilder> ConfigureHostCallback { get; set; }

        /// <summary>
        /// Creates and configures the <see cref="IHost"/> of this <see cref="IMinimalHostFixture"/>.
        /// </summary>
        /// <param name="hostTest">The object that inherits from <see cref="MinimalHostTest{T}"/>.</param>
        /// <remarks><paramref name="hostTest"/> was added to support those cases where the caller is required in the host configuration.</remarks>
        void ConfigureHost(Test hostTest);
    }
}
