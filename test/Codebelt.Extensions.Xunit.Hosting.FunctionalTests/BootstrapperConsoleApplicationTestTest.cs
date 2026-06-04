using Xunit;
using BootstrapperConsoleMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperConsole.App.BootstrapperConsoleMarker;
using BootstrapperConsoleProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperConsole.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting;

public class BootstrapperConsoleApplicationTestTest : ApplicationTest<BootstrapperConsoleProgram, ManagedApplicationFixture<BootstrapperConsoleProgram>>
{
    public BootstrapperConsoleApplicationTestTest(ManagedApplicationFixture<BootstrapperConsoleProgram> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public void ShouldBootstrapLegacyConsoleProgramAndStartup()
    {
        Assert.Equal("Bootstrapper Console", BootstrapperConsoleMarker.LastValue);
        Assert.Equal("Development", Environment.EnvironmentName);
        Assert.NotNull(Host);
    }
}
