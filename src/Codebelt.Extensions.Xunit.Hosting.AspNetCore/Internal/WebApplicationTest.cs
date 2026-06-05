using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Internal;

internal sealed class WebApplicationTest<TEntryPoint> : WebApplicationTest<TEntryPoint, IWebApplicationFixture<TEntryPoint>> where TEntryPoint : class
{
    private readonly Action<IWebHostBuilder> _webHostConfigurator;
    private readonly IWebApplicationFixture<TEntryPoint> _hostFixture;

    internal WebApplicationTest(Action<IWebHostBuilder> webHostConfigurator, IWebApplicationFixture<TEntryPoint> hostFixture) : base(true, hostFixture, callerType: webHostConfigurator?.Target?.GetType())
    {
        _webHostConfigurator = webHostConfigurator;
        _hostFixture = hostFixture;
        InitializeHostFixture(hostFixture);
    }

    private void InitializeHostFixture(IWebApplicationFixture<TEntryPoint> hostFixture)
    {
        if (!hostFixture.HasValidState())
        {
            hostFixture.ConfigureCallback = Configure;
            hostFixture.ConfigureWebHostCallback = ConfigureWebHost;
            hostFixture.ConfigureHost(this);
        }
        Host = hostFixture.Host;
        Server = hostFixture.Server;
        Configure(hostFixture.Configuration, hostFixture.Environment);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _webHostConfigurator?.Invoke(builder);
    }

    protected override void OnDisposeManagedResources()
    {
        _hostFixture.Dispose();
        base.OnDisposeManagedResources();
    }

    protected override async ValueTask OnDisposeManagedResourcesAsync()
    {
        await _hostFixture.DisposeAsync().ConfigureAwait(false);
        await base.OnDisposeManagedResourcesAsync().ConfigureAwait(false);
    }
}
