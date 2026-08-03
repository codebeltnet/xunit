using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.Program.App;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using BootstrapperMinimalWebProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWeb.App.Program;
using BootstrapperWebProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperWeb.App.Program;
using Classic = Codebelt.Extensions.Xunit.Hosting.ClassicProgram.App.Program;
using ClassicProgramState = Codebelt.Extensions.Xunit.Hosting.ClassicProgram.App.ClassicProgramState;
using ModernProgram = Codebelt.Extensions.Xunit.Hosting.Program.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class WebApplicationTestFactoryTest : Test
{
    public WebApplicationTestFactoryTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Create_ShouldBootstrapApplication_WhenEntryPointUsesBootstrapperMinimalWebProgram()
    {
        using var application = WebApplicationTestFactory.Create<BootstrapperMinimalWebProgram>();
        using var client = application.Host.GetTestClient();

        var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Bootstrapper Minimal Web", body);
    }

    [Fact]
    public async Task Create_ShouldBootstrapApplication_WhenEntryPointUsesBootstrapperWebProgram()
    {
        using var application = WebApplicationTestFactory.Create<BootstrapperWebProgram>();
        using var client = application.Host.GetTestClient();

        var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Bootstrapper Web|Development", body);
    }

    [Fact]
    public async Task Create_ShouldBootstrapApplication_WhenEntryPointUsesClassicProgram()
    {
        using var application = WebApplicationTestFactory.Create<Classic>(hostFixture: new ManagedWebApplicationFixture<Classic>());
        using var client = application.Host.GetTestClient();

        var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Classic Program", body);
        var state = application.Host.Services.GetRequiredService<ClassicProgramState>();
        Assert.True(state.MainInvoked);
        Assert.True(state.EntrypointStarted);
    }

    [Fact]
    public async Task Create_ShouldBootstrapApplication_WhenEntryPointUsesModernProgramPattern()
    {
        using var application = WebApplicationTestFactory.Create<ModernProgram>();
        using var client = application.Host.GetTestClient();

        var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Modern Program|Development", body);
    }

    [Fact]
    public async Task Create_ShouldApplyWebHostConfiguration_WhenWebHostSetupIsProvided()
    {
        using var application = WebApplicationTestFactory.Create<ModernProgram>(builder =>
        {
            builder.ConfigureAppConfiguration((_, configuration) =>
            {
                configuration.AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["ProgramLane:Message"] = "Configured from WebApplicationTestFactory"
                });
            });

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(new ProgramCustomization("Custom service from WebApplicationTestFactory"));
            });
        });
        using var client = application.Host.GetTestClient();

        var configurationResponse = await client.GetAsync("/configuration").ConfigureAwait(false);
        var configurationBody = await configurationResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
        var serviceResponse = await client.GetAsync("/custom-service").ConfigureAwait(false);
        var serviceBody = await serviceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(configurationResponse.IsSuccessStatusCode);
        Assert.Equal("Configured from WebApplicationTestFactory", configurationBody);
        Assert.True(serviceResponse.IsSuccessStatusCode);
        Assert.Equal("Custom service from WebApplicationTestFactory", serviceBody);
    }

    [Fact]
    public async Task Create_ShouldSupportExplicitBlockingFixture()
    {
        using var application = WebApplicationTestFactory.Create<Classic>(hostFixture: new BlockingManagedWebApplicationFixture<Classic>());
        using var client = application.Host.GetTestClient();

        using var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Classic Program", body);
        Assert.False(application.Host.Services.GetRequiredService<ClassicProgramState>().MainInvoked);
    }

    [Fact]
    public async Task RunAsync_ShouldReturnResponse_WhenEntryPointUsesModernProgramPattern()
    {
        using var response = await WebApplicationTestFactory.RunAsync<ModernProgram>().ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Modern Program|Development", body);
    }

    [Fact]
    public void Create_CallerTypeShouldHaveDeclaringTypeOfWebApplicationTestFactoryTest()
    {
        Type sut = GetType();
        using var application = WebApplicationTestFactory.Create<ModernProgram>(_ =>
        {
        });

        Assert.True(sut == application.CallerType.DeclaringType);
    }
}
