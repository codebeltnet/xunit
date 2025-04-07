using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Provides a way to support app and lifetime management in unit tests.
    /// </summary>
    /// <seealso cref="IConfigurationTest" />
    /// <seealso cref="IEnvironmentTest" />
    /// <seealso cref="IDisposable"/>
    /// <seealso cref="IAsyncDisposable"/>
    public interface IHostFixture : IConfigurationTest, IEnvironmentTest, IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Gets the <see cref="IHost"/> initialized by either the <see cref="IGenericHostFixture"/> or <see cref="IMinimalHostFixture"/>.
        /// </summary>
        /// <value>The <see cref="IHost"/> initialized by the <see cref="IGenericHostFixture"/> or <see cref="IMinimalHostFixture"/>.</value>
        IHost Host { get; }

        /// <summary>
        /// Gets or sets the delegate that adds configuration and environment information to a <see cref="HostTest"/>.
        /// </summary>
        /// <value>The delegate that adds configuration and environment information to a <see cref="HostTest"/>.</value>
        Action<IConfiguration, IHostEnvironment> ConfigureCallback { get; set; }
    }
}
