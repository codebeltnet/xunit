using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using BootstrapperMinimalConsoleProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App.Program;
using BootstrapperMinimalConsoleState = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App.BootstrapperMinimalConsoleState;

namespace Codebelt.Extensions.Xunit.Hosting;

public class ApplicationHostFactoryTest : Test
{
    public ApplicationHostFactoryTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task ShouldUseFrameworkHostAbortedException_WhenStoppingEntrypointAfterHostIsBuilt()
    {
        var context = new AsyncLocal<bool> { Value = true };
        var observedException = new TaskCompletionSource<Exception>(TaskCreationOptions.RunContinuationsAsynchronously);
        EventHandler<FirstChanceExceptionEventArgs> observer = (_, args) =>
        {
            if (context.Value && args.Exception.GetType().Name == nameof(HostAbortedException))
            {
                observedException.TrySetResult(args.Exception);
            }
        };

        AppDomain.CurrentDomain.FirstChanceException += observer;
        try
        {
            using var host = ApplicationHostFactory.Create<BootstrapperMinimalConsoleProgram>(null);

            await host.StartAsync().WaitAsync(TimeSpan.FromSeconds(10));
            var exception = await observedException.Task.WaitAsync(TimeSpan.FromSeconds(10));

            Assert.IsType<HostAbortedException>(exception);
            Assert.False(host.Services.GetRequiredService<BootstrapperMinimalConsoleState>().EntrypointStarted);
        }
        finally
        {
            AppDomain.CurrentDomain.FirstChanceException -= observer;
        }
    }
}
