using System.Threading.Tasks;
using Codebelt.Bootstrapper.Worker;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperWorker.App;

public sealed class Program : WorkerProgram<Startup>
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync().ConfigureAwait(false);
    }
}
