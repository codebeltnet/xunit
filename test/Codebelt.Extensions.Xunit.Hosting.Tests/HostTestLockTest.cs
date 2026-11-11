using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Xunit;
#if NET10_0_OR_GREATER
using Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App;
using BootstrapperMinimalConsoleProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App.Program;
#endif

namespace Codebelt.Extensions.Xunit.Hosting;

public class HostTestLockTest : Test
{
    public HostTestLockTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void ShouldReturnNull_WhenHostIsNotAssigned()
    {
        using var test = new HostTestProbe();

        Assert.Null(test.Host);
    }

#if NET10_0_OR_GREATER
    [Fact]
    public async ValueTask ShouldStartDeferredHost_WhenAccessedConcurrently()
    {
        using var test = new HostTestProbe();
        using var fixture = new ManagedApplicationFixture<BootstrapperMinimalConsoleProgram>();
        fixture.ConfigureCallback = (_, _) => { };
        fixture.ConfigureHost(test);
        var host = fixture.Host;
        var state = (BootstrapperMinimalConsoleState)host.Services.GetService(typeof(BootstrapperMinimalConsoleState));
        Assert.NotNull(state);
        Assert.False(state.EntrypointStarted);
        test.SetHost(host);

        var results = await Task.WhenAll(Enumerable.Range(0, 8).Select(_ => Task.Run(() => test.Host)));

        Assert.All(results, result => Assert.Same(host, result));
        Assert.True(state.EntrypointStarted);
    }
#endif

    private sealed class HostTestProbe : HostTest
    {
        public void SetHost(IHost host)
        {
            Host = host;
        }
    }
}
