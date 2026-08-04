using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using ClassicProgramState = Codebelt.Extensions.Xunit.Hosting.ClassicProgram.App.ClassicProgramState;
using Classic = Codebelt.Extensions.Xunit.Hosting.ClassicProgram.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class StartupValidationWebApplicationTestTest : Test
{
    public StartupValidationWebApplicationTestTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void ShouldPropagateStartupValidationFailure_WhenUsingManagedWebApplicationFixture()
    {
        var missing = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var state = new ClassicProgramState();
        var validation = new StartupValidationService(state, missing);
        using var fixture = new ManagedWebApplicationFixture<Classic>();
        using var application = WebApplicationTestFactory.Create<Classic>(
            builder => ConfigureStartupValidation(builder, state, validation),
            fixture);

        var exception = Assert.ThrowsAny<Exception>(() => _ = application.Host.Services);

        Assert.Contains("content root", exception.ToString(), StringComparison.OrdinalIgnoreCase);
        Assert.True(state.MainInvoked);
        Assert.True(validation.Started);
    }

    [Fact]
    public void ShouldNotInvokeEntrypoint_WhenUsingBlockingManagedWebApplicationFixture()
    {
        var missing = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var state = new ClassicProgramState();
        var validation = new StartupValidationService(state, missing);
        using var fixture = new BlockingManagedWebApplicationFixture<Classic>();
        using var application = WebApplicationTestFactory.Create<Classic>(
            builder => ConfigureStartupValidation(builder, state, validation),
            fixture);

        Assert.NotNull(application.Host.Services);
        Assert.False(state.MainInvoked);
        Assert.True(validation.Started);
    }

    private static void ConfigureStartupValidation(IWebHostBuilder builder, ClassicProgramState state, StartupValidationService validation)
    {
        builder.ConfigureLogging(logging => logging.ClearProviders());
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(state);
            services.AddSingleton<IHostedService>(validation);
        });
    }

    private sealed class StartupValidationService : IHostedService
    {
        private readonly string _contentRoot;
        private readonly ClassicProgramState _state;

        public StartupValidationService(ClassicProgramState state, string contentRoot)
        {
            _state = state;
            _contentRoot = contentRoot;
        }

        public bool Started { get; private set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Started = true;
            if (_state.MainInvoked && !Directory.Exists(_contentRoot))
            {
                throw new InvalidOperationException($"Invalid startup configuration. The content root '{_contentRoot}' does not exist.");
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
