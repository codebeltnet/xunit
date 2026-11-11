using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using BootstrapperMinimalConsoleProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting;

public class DeferredHostCancellationTest : Test
{
    public DeferredHostCancellationTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task ShouldPreserveCancellationToken_WhenWaitingForEntrypointStartupIsCanceled()
    {
        var startup = new BlockingStartupService();
        using var host = ApplicationHostFactory.Create<BootstrapperMinimalConsoleProgram>(
            builder => builder.ConfigureServices(services => services.AddSingleton<IHostedService>(startup)), false);
        using var cancellation = new CancellationTokenSource();

        try
        {
            await startup.Starting.Task.WaitAsync(TimeSpan.FromSeconds(10));
            var start = host.StartAsync(cancellation.Token);
            cancellation.Cancel();

            var exception = await Assert.ThrowsAnyAsync<OperationCanceledException>(() => start);
            Assert.Equal(cancellation.Token, exception.CancellationToken);
        }
        finally
        {
            var started = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var stopped = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
            using var startedRegistration = lifetime.ApplicationStarted.Register(() => started.TrySetResult(true));
            using var stoppedRegistration = lifetime.ApplicationStopped.Register(() => stopped.TrySetResult(true));
            startup.Continue.TrySetResult(true);
            await started.Task.WaitAsync(TimeSpan.FromSeconds(10));
            lifetime.StopApplication();
            await stopped.Task.WaitAsync(TimeSpan.FromSeconds(10));
        }
    }

    private sealed class BlockingStartupService : IHostedService
    {
        public TaskCompletionSource<bool> Starting { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public TaskCompletionSource<bool> Continue { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Starting.TrySetResult(true);
            await Continue.Task;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
