using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.Program.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ModernProgram = Codebelt.Extensions.Xunit.Hosting.Program.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class WebApplicationTestTest : WebApplicationTest<ModernProgram, ManagedWebApplicationFixture<ModernProgram>>
{
    public WebApplicationTestTest(ManagedWebApplicationFixture<ModernProgram> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public async Task ShouldBootstrapApplication_WhenEntryPointUsesModernProgramPattern()
    {
        using var client = Host.GetTestClient();

        var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Modern Program|Development", body);
    }

    [Fact]
    public async Task ShouldApplyWebHostConfiguration_WhenConfigureWebHostIsOverridden()
    {
        using var client = Host.GetTestClient();

        var response = await client.GetAsync("/configuration").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Configured from WebApplicationTest", body);
    }

    [Fact]
    public async Task ShouldAddServices_WhenConfigureWebHostIsOverridden()
    {
        using var client = Server.CreateClient();

        var response = await client.GetAsync("/custom-service").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Custom service", body);
    }

    [Fact]
    public void ShouldExposeHostConfigurationEnvironmentAndServer()
    {
        Assert.NotNull(Host);
        Assert.NotNull(Configuration);
        Assert.NotNull(Environment);
        Assert.NotNull(Server);
        Assert.NotNull(Server.Services.GetRequiredService<ProgramMarker>());
        Assert.Equal("Development", Environment.EnvironmentName);
    }

    [Fact]
    public void ShouldHaveValidFixtureState_WhenApplicationIsBootstrapped()
    {
        var fixture = new ManagedWebApplicationFixture<ModernProgram>();
        var test = new DeferredModernWebApplicationTest(fixture);

        fixture.ConfigureCallback = test.Configure;
        fixture.ConfigureWebHostCallback = test.Configure;
        fixture.ConfigureHost(test);

        Assert.True(fixture.HasValidState());
    }

    [Fact]
    public void Test_VerifyAbstractions()
    {
        Assert.IsAssignableFrom<IHostTest>(this);
        Assert.IsAssignableFrom<IConfigurationTest>(this);
        Assert.IsAssignableFrom<IEnvironmentTest>(this);
        Assert.IsAssignableFrom<ITest>(this);
        Assert.IsAssignableFrom<IDisposable>(this);
        Assert.IsAssignableFrom<IAsyncDisposable>(this);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, configuration) =>
        {
            configuration.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["ProgramLane:Message"] = "Configured from WebApplicationTest"
            });
        });

        builder.ConfigureServices(services =>
        {
            services.AddSingleton(new ProgramCustomization("Custom service"));
        });
    }
}
