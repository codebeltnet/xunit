using Codebelt.Bootstrapper.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperWeb.App;

public sealed class Program : WebProgram<Startup>
{
    public static bool MainInvoked { get; private set; }

    public static bool EntrypointStarted { get; private set; }

    public static void Main(string[] args)
    {
        MainInvoked = true;
        var host = CreateHostBuilder(args).Build();
        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() => EntrypointStarted = true);
        host.Run();
    }
}
