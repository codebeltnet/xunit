using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App;

public sealed class BootstrapperMinimalWorkerService : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
