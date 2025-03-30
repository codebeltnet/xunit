using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Extends the default implementation of the <see cref="IAspNetCoreHostFixture"/> interface to be synchronous e.g., blocking where exceptions can be captured.
    /// </summary>
    /// <seealso cref="AspNetCoreHostFixture" />
    public class BlockingAspNetCoreHostFixture : AspNetCoreHostFixture
    {
        /// <summary>
        /// Starts the by <see cref="AspNetCoreHostFixture.ConfigureHost"/> initialized <see cref="IHost"/>.
        /// </summary>
        /// <remarks><see cref="AspNetCoreHostFixture.ConfigureHost"/> is responsible for configuring and setting the <see cref="Host"/> property.</remarks>
        protected override void StartConfiguredHost()
        {
            Host.Start();
        }
    }
}
