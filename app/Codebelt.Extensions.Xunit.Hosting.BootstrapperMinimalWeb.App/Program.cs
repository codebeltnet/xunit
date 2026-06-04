using System.Threading.Tasks;
using Codebelt.Bootstrapper.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWeb.App;

public sealed class Program : MinimalWebProgram
{
    public static Task Main(string[] args)
    {
        var builder = CreateHostBuilder(args);
        builder.Services.AddSingleton(new BootstrapperMinimalWebMarker("Bootstrapper Minimal Web"));

        var app = builder.Build();

        app.MapGet("/", (BootstrapperMinimalWebMarker marker) => marker.Value);

        return app.RunAsync();
    }
}
