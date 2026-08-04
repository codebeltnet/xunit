using System.Threading.Tasks;
using Codebelt.Bootstrapper.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWeb.App;

public sealed class Program : MinimalWebProgram
{
    public static bool MainInvoked { get; private set; }

    public static bool EntrypointStarted { get; private set; }

    public static Task Main(string[] args)
    {
        MainInvoked = true;
        var builder = CreateHostBuilder(args);
        builder.Services.AddSingleton(new BootstrapperMinimalWebMarker("Bootstrapper Minimal Web"));

        var app = builder.Build();
        app.Lifetime.ApplicationStarted.Register(() => EntrypointStarted = true);

        app.MapGet("/", (BootstrapperMinimalWebMarker marker) => marker.Value);

        return app.RunAsync();
    }
}
