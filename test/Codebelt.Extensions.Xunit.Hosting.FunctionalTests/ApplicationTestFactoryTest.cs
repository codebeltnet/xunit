using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using BootstrapperConsoleMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperConsole.App.BootstrapperConsoleMarker;
using BootstrapperConsoleProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperConsole.App.Program;
using BootstrapperMinimalConsoleProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App.Program;
using BootstrapperMinimalConsoleState = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App.BootstrapperMinimalConsoleState;
using BootstrapperMinimalWorkerMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App.BootstrapperMinimalWorkerMarker;
using BootstrapperMinimalWorkerProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App.Program;
using BootstrapperWorkerMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperWorker.App.BootstrapperWorkerMarker;
using BootstrapperWorkerProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperWorker.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting;

public class ApplicationTestFactoryTest : Test
{
    public ApplicationTestFactoryTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void Create_ShouldBootstrapApplication_WhenEntryPointUsesBootstrapperConsoleProgram()
    {
        using var application = ApplicationTestFactory.Create<BootstrapperConsoleProgram>();

        var marker = application.Host.Services.GetRequiredService<BootstrapperConsoleMarker>();

        Assert.Equal("Bootstrapper Console", marker.Value);
        Assert.Equal("Development", application.Environment.EnvironmentName);
        Assert.NotNull(application.Host);
    }

    [Fact]
    public void Create_ShouldStartEntrypoint_WhenUsingManagedApplicationFixture()
    {
        using var application = ApplicationTestFactory.Create<BootstrapperConsoleProgram>(hostFixture: new ManagedApplicationFixture<BootstrapperConsoleProgram>());
        var services = application.Host.Services;
        var lifetime = services.GetRequiredService<IHostApplicationLifetime>();
        var marker = services.GetRequiredService<BootstrapperConsoleMarker>();

        Assert.Equal("Bootstrapper Console", marker.Value);
        Assert.True(BootstrapperConsoleProgram.MainInvoked);
        Assert.True(lifetime.ApplicationStarted.IsCancellationRequested);
    }

    [Fact]
    public void Create_ShouldBootstrapApplication_WhenEntryPointUsesMinimalConsoleProgram()
    {
        using var application = ApplicationTestFactory.Create<BootstrapperMinimalConsoleProgram>();
        var state = application.Host.Services.GetRequiredService<BootstrapperMinimalConsoleState>();

        Assert.Equal("Development", application.Environment.EnvironmentName);
        Assert.NotNull(application.Host);
        Assert.True(state.MainInvoked);
        Assert.True(state.EntrypointStarted);
    }

    [Fact]
    public void Create_ShouldBootstrapApplication_WhenEntryPointUsesBootstrapperMinimalWorkerProgram()
    {
        using var application = ApplicationTestFactory.Create<BootstrapperMinimalWorkerProgram>();

        var marker = application.Host.Services.GetRequiredService<BootstrapperMinimalWorkerMarker>();

        Assert.Equal("Bootstrapper Minimal Worker", marker.Value);
        Assert.Equal("Development", application.Environment.EnvironmentName);
        Assert.NotNull(application.Host.Services.GetRequiredService<IHostApplicationLifetime>());
    }

    [Fact]
    public void Create_ShouldBootstrapApplication_WhenEntryPointUsesBootstrapperWorkerProgram()
    {
        using var application = ApplicationTestFactory.Create<BootstrapperWorkerProgram>();

        var marker = application.Host.Services.GetRequiredService<BootstrapperWorkerMarker>();

        Assert.Equal("Bootstrapper Worker", marker.Value);
        Assert.Equal("Development", application.Environment.EnvironmentName);
        Assert.NotNull(application.Host.Services.GetRequiredService<IHostApplicationLifetime>());
    }

    [Fact]
    public void Create_ShouldApplyHostConfiguration_WhenHostSetupIsProvided()
    {
        using var application = ApplicationTestFactory.Create<BootstrapperMinimalConsoleProgram>(host =>
        {
            host.ConfigureAppConfiguration((_, configuration) =>
            {
                configuration.AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["Factory:Message"] = "Configured from ApplicationTestFactory"
                });
            });
        });

        Assert.Equal("Configured from ApplicationTestFactory", application.Configuration["Factory:Message"]);
    }

    [Fact]
    public void Create_CallerTypeShouldHaveDeclaringTypeOfApplicationTestFactoryTest()
    {
        Type sut = GetType();
        using (var application = ApplicationTestFactory.Create<BootstrapperMinimalConsoleProgram>(_ =>
        {
        }))
        {
            Assert.True(sut == application.CallerType.DeclaringType);
        }
    }

    [Fact]
    public async Task Create_ShouldDisposeFixtureAsync_WhenApplicationIsDisposedAsync()
    {
        await using var application = ApplicationTestFactory.Create<BootstrapperMinimalConsoleProgram>();

        Assert.NotNull(application.Host);
    }
}
