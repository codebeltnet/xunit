using System;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.Internal;

internal static class HostBuilderFactory
{
    public static IHost Build(IHostBuilder hostBuilder, Action<IHostBuilder> configureHost)
    {
        configureHost?.Invoke(hostBuilder);

#if NET9_0_OR_GREATER
        hostBuilder.UseDefaultServiceProvider(o =>
        {
            o.ValidateOnBuild = true;
            o.ValidateScopes = true;
        });
#endif

        var host = hostBuilder.Build();
        if (hostBuilder is IDisposable disposable)
        {
            disposable.Dispose();
        }

        return host;
    }
}
