using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.ClassicProgram.App;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var state = host.Services.GetRequiredService<ClassicProgramState>();
        state.MainInvoked = true;
        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() => state.EntrypointStarted = true);
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<ClassicProgramState>();
                    services.AddSingleton(new ClassicProgramMarker("Classic Program"));
                });
                builder.Configure(app => app.Run(async context =>
                {
                    var marker = context.RequestServices.GetRequiredService<ClassicProgramMarker>();
                    await context.Response.WriteAsync(marker.Value).ConfigureAwait(false);
                }));
            });
    }
}
