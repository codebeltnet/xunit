using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Classic = Codebelt.Extensions.Xunit.Hosting.ClassicProgram.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class ClassicWebApplicationTestTest : WebApplicationTest<Classic, ManagedWebApplicationFixture<Classic>>
{
    public ClassicWebApplicationTestTest(ManagedWebApplicationFixture<Classic> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public async Task ShouldBootstrapApplication_WhenEntryPointExposesCreateHostBuilder()
    {
        using var client = Host.GetTestClient();

        var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Classic Program", body);
    }
}
