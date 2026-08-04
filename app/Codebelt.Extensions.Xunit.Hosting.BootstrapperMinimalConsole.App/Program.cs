using System;
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App;

public sealed class Program : MinimalConsoleProgram<Program>
{
    public static Task Main(string[] args)
    {
        var builder = CreateHostBuilder(args);
        var state = new BootstrapperMinimalConsoleState { MainInvoked = true };
        builder.Services.AddSingleton(state);
        builder.Services.AddSingleton(new BootstrapperMinimalConsoleMarker("Bootstrapper Minimal Console"));

        var host = builder.Build();
        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() => state.EntrypointStarted = true);
        return host.RunAsync();
    }

    public override async Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(Timeout.Infinite, cancellationToken).ConfigureAwait(false);
        } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
    }
}
