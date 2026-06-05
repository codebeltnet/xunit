using Codebelt.Bootstrapper.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperWeb.App;

public sealed class Startup : WebStartup
{
    public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(new BootstrapperWebMarker("Bootstrapper Web"));
    }

    public override void ConfigurePipeline(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                var marker = context.RequestServices.GetRequiredService<BootstrapperWebMarker>();
                await context.Response.WriteAsync($"{marker.Value}|{Environment.EnvironmentName}").ConfigureAwait(false);
            });
        });
    }
}
