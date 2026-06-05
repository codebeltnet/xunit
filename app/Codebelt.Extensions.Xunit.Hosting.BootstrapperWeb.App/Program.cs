using Codebelt.Bootstrapper.Web;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperWeb.App;

public sealed class Program : WebProgram<Startup>
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
}
