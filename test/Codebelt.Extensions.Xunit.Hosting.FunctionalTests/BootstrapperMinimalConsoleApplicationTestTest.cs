using Xunit;
using BootstrapperMinimalConsoleMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App.BootstrapperMinimalConsoleMarker;
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
        Assert.Equal("Bootstrapper Minimal Console", BootstrapperMinimalConsoleMarker.LastValue);
        Assert.Equal("Development", Environment.EnvironmentName);
        Assert.NotNull(Host);
    }
}
