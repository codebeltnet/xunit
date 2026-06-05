using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.Internal;

internal sealed class ApplicationTest<TEntryPoint> : ApplicationTest<TEntryPoint, IApplicationFixture<TEntryPoint>> where TEntryPoint : class
{
    private readonly Action<IHostBuilder> _hostConfigurator;
    private readonly IApplicationFixture<TEntryPoint> _hostFixture;

    internal ApplicationTest(Action<IHostBuilder> hostConfigurator, IApplicationFixture<TEntryPoint> hostFixture) : base(true, hostFixture, callerType: hostConfigurator?.Target?.GetType())
    {
        _hostConfigurator = hostConfigurator;
        _hostFixture = hostFixture;
        InitializeHostFixture(hostFixture);
    }

    private void InitializeHostFixture(IApplicationFixture<TEntryPoint> hostFixture)
    {
        if (!hostFixture.HasValidState())
        {
            hostFixture.ConfigureCallback = Configure;
            hostFixture.ConfigureHostCallback = ConfigureHost;
            hostFixture.ConfigureHost(this);
        }
        Host = hostFixture.Host;
        Configure(hostFixture.Configuration, hostFixture.Environment);
    }

    protected override void ConfigureHost(IHostBuilder builder)
    {
        _hostConfigurator?.Invoke(builder);
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
