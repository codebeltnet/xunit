using System.Threading.Tasks;
using Codebelt.Bootstrapper.Console;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperConsole.App;

public sealed class Program : ConsoleProgram<Startup>
{
    public static Task Main(string[] args)
    {
        return CreateHostBuilder(args).Build().RunAsync();
    }
}
