using Codebelt.Bootstrapper.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperWorker.App;

public sealed class Startup : WorkerStartup
{
    public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(new BootstrapperWorkerMarker("Bootstrapper Worker"));
        services.AddHostedService<BootstrapperWorkerService>();
    }
}
