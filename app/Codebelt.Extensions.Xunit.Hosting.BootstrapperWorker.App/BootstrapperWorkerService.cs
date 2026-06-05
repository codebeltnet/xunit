using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperWorker.App;

public sealed class BootstrapperWorkerService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
