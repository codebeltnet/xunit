using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using BootstrapperMinimalConsoleProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting;

public class BootstrapperMinimalConsoleApplicationTestTest : ApplicationTest<BootstrapperMinimalConsoleProgram, ManagedApplicationFixture<BootstrapperMinimalConsoleProgram>>
{
    public BootstrapperMinimalConsoleApplicationTestTest(ManagedApplicationFixture<BootstrapperMinimalConsoleProgram> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public void ShouldBootstrapMinimalConsoleProgram()
    {
        var marker = Host.Services.GetRequiredService<BootstrapperMinimalConsoleMarker>();
        Assert.Equal("Bootstrapper Minimal Console", marker.Value);
        Assert.Equal("Development", Environment.EnvironmentName);
        Assert.NotNull(Host);
    }

    [Fact]
    public async Task ShouldCompleteRunAsync_WhenCancellationIsRequested()
    {
        using var cancellation = new CancellationTokenSource();
        var run = new BootstrapperMinimalConsoleProgram().RunAsync(Host.Services, cancellation.Token);
        cancellation.Cancel();

        await run.ConfigureAwait(false);
    }
}
