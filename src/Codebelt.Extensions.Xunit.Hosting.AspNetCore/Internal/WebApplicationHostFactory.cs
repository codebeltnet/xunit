using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Internal;

internal static class WebApplicationHostFactory
{
    public static IHost Create<TEntryPoint>(Action<IWebHostBuilder> configureWebHost) where TEntryPoint : class
    {
        return ApplicationHostFactory.Create<TEntryPoint>(hostBuilder => hostBuilder.ConfigureWebHost(webHostBuilder =>
        {
            webHostBuilder.UseTestServer(o => o.PreserveExecutionContext = true);
            configureWebHost?.Invoke(webHostBuilder);
        }), false);
    }
}
