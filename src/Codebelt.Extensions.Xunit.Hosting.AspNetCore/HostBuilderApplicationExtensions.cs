using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Provides extension methods for <see cref="IHostApplicationBuilder"/>.
    /// </summary>
    public static class HostBuilderApplicationExtensions
    {
        /// <summary>
        /// Converts an <see cref="IHostApplicationBuilder"/> to an <see cref="IHostBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to convert.</param>
        /// <returns>The <see cref="IHostBuilder"/> instance.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="builder"/> is not a <see cref="WebApplicationBuilder"/>.
        /// </exception>
        public static IHostBuilder ToHostBuilder(this IHostApplicationBuilder builder)
        {
            if (builder is WebApplicationBuilder webAppBuilder) { return webAppBuilder.Host; }
            throw new ArgumentException($"The provided IHostApplicationBuilder is not a {nameof(WebApplicationBuilder)}.", nameof(builder));
        }
    }
}
