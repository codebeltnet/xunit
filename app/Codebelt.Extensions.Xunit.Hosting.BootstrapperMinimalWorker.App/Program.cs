using System.Threading.Tasks;
using Codebelt.Bootstrapper.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App;

public sealed class Program : MinimalWorkerProgram
{
    public static bool MainInvoked { get; private set; }

    public static bool EntrypointStarted { get; private set; }

    public static Task Main(string[] args)
    {
        MainInvoked = true;
        var builder = CreateHostBuilder(args);
        builder.Services.AddSingleton(new BootstrapperMinimalWorkerMarker("Bootstrapper Minimal Worker"));
        builder.Services.AddHostedService<BootstrapperMinimalWorkerService>();

        var host = builder.Build();
        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() => EntrypointStarted = true);
        return host.RunAsync();
    }
}
