using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using BootstrapperWebMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperWeb.App.BootstrapperWebMarker;
using BootstrapperWebProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperWeb.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class BootstrapperWebApplicationTestTest : WebApplicationTest<BootstrapperWebProgram, ManagedWebApplicationFixture<BootstrapperWebProgram>>
{
    public BootstrapperWebApplicationTestTest(ManagedWebApplicationFixture<BootstrapperWebProgram> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public async Task ShouldBootstrapLegacyWebProgramAndStartup()
    {
        using var client = Host.GetTestClient();

        var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Bootstrapper Web|Development", body);
        Assert.Equal("Bootstrapper Web", Host.Services.GetRequiredService<BootstrapperWebMarker>().Value);
    }
}
