using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperConsole.App;

public sealed class Program : ConsoleProgram<Startup>
{
    public static bool MainInvoked { get; private set; }

    public static bool EntrypointStarted { get; private set; }

    public static Task Main(string[] args)
    {
        MainInvoked = true;
        var host = CreateHostBuilder(args).Build();
        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() => EntrypointStarted = true);
        return host.RunAsync();
    }
}
