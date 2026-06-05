using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using BootstrapperMinimalWorkerMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App.BootstrapperMinimalWorkerMarker;
using BootstrapperMinimalWorkerProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting;

public class BootstrapperMinimalWorkerApplicationTestTest : ApplicationTest<BootstrapperMinimalWorkerProgram, BlockingManagedApplicationFixture<BootstrapperMinimalWorkerProgram>>
{
    public BootstrapperMinimalWorkerApplicationTestTest(BlockingManagedApplicationFixture<BootstrapperMinimalWorkerProgram> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public void ShouldBootstrapMinimalWorkerProgram()
    {
        var marker = Host.Services.GetRequiredService<BootstrapperMinimalWorkerMarker>();

        Assert.Equal("Bootstrapper Minimal Worker", marker.Value);
        Assert.Equal("Development", Environment.EnvironmentName);
        Assert.NotNull(Host.Services.GetRequiredService<IHostApplicationLifetime>());
    }
}
