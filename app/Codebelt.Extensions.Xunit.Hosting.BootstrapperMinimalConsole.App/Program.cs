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
        builder.Services.AddSingleton(new BootstrapperMinimalConsoleMarker("Bootstrapper Minimal Console"));

        var host = builder.Build();
        return host.RunAsync();
    }

    public override Task RunAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        BootstrapperMinimalConsoleMarker.LastValue = serviceProvider.GetRequiredService<BootstrapperMinimalConsoleMarker>().Value;
        return Task.CompletedTask;
    }
}
