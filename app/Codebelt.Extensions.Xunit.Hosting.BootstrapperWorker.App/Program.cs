using System.Threading.Tasks;
using Codebelt.Bootstrapper.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperWorker.App;

public sealed class Program : WorkerProgram<Startup>
{
    public static bool MainInvoked { get; private set; }

    public static bool EntrypointStarted { get; private set; }

    public static async Task Main(string[] args)
    {
        MainInvoked = true;
        var host = CreateHostBuilder(args).Build();
        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() => EntrypointStarted = true);
        await host.RunAsync().ConfigureAwait(false);
    }
}
