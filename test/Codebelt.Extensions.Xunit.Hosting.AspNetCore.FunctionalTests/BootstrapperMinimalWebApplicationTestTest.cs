using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using BootstrapperMinimalWebMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWeb.App.BootstrapperMinimalWebMarker;
using BootstrapperMinimalWebProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWeb.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class BootstrapperMinimalWebApplicationTestTest : WebApplicationTest<BootstrapperMinimalWebProgram, BlockingManagedWebApplicationFixture<BootstrapperMinimalWebProgram>>
{
    public BootstrapperMinimalWebApplicationTestTest(BlockingManagedWebApplicationFixture<BootstrapperMinimalWebProgram> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public async Task ShouldBootstrapMinimalWebProgram()
    {
        using var client = Server.CreateClient();

        var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Bootstrapper Minimal Web", body);
        Assert.Equal("Bootstrapper Minimal Web", Host.Services.GetRequiredService<BootstrapperMinimalWebMarker>().Value);
    }
}
