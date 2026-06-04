using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using BootstrapperWorkerMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperWorker.App.BootstrapperWorkerMarker;
using BootstrapperWorkerProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperWorker.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting;

public class BootstrapperWorkerApplicationTestTest : ApplicationTest<BootstrapperWorkerProgram, ManagedApplicationFixture<BootstrapperWorkerProgram>>
{
    public BootstrapperWorkerApplicationTestTest(ManagedApplicationFixture<BootstrapperWorkerProgram> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public void ShouldBootstrapLegacyWorkerProgramAndStartup()
    {
        var marker = Host.Services.GetRequiredService<BootstrapperWorkerMarker>();

        Assert.Equal("Bootstrapper Worker", marker.Value);
        Assert.Equal("Development", Environment.EnvironmentName);
        Assert.NotNull(Host.Services.GetRequiredService<IHostApplicationLifetime>());
    }
}
