using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using BootstrapperMinimalConsoleProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting;

public class StartupValidationApplicationTestTest : Test
{
    public StartupValidationApplicationTestTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void ShouldPropagateStartupValidationFailure_WhenUsingManagedApplicationFixture()
    {
        var missing = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var validation = new StartupValidationService(missing);
        using var fixture = new ManagedApplicationFixture<BootstrapperMinimalConsoleProgram>();
        using var application = ApplicationTestFactory.Create<BootstrapperMinimalConsoleProgram>(
            builder => ConfigureStartupValidation(builder, validation),
            fixture);

        var exception = Assert.ThrowsAny<Exception>(() => _ = application.Host.Services);

        Assert.Contains("content root", exception.ToString(), StringComparison.OrdinalIgnoreCase);
        Assert.True(validation.Started);
    }

    private static void ConfigureStartupValidation(IHostBuilder builder, StartupValidationService validation)
    {
        builder.ConfigureLogging(logging => logging.ClearProviders());
        builder.ConfigureServices((_, services) => services.AddSingleton<IHostedService>(validation));
    }

    private sealed class StartupValidationService : IHostedService
    {
        private readonly string _contentRoot;

        public StartupValidationService(string contentRoot)
        {
            _contentRoot = contentRoot;
        }

        public bool Started { get; private set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Started = true;
            if (!Directory.Exists(_contentRoot))
            {
                throw new InvalidOperationException($"Invalid startup configuration. The content root '{_contentRoot}' does not exist.");
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
