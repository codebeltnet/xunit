using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Assets
{
    public class ValidHostTest : AspNetCoreHostTest<AspNetCoreHostFixture>
    {
        public ValidHostTest(AspNetCoreHostFixture hostFixture) : base(hostFixture)
        {
            if (!hostFixture.HasValidState())
            {
                hostFixture.ConfigureHostCallback = ConfigureHost;
                hostFixture.ConfigureCallback = Configure;
                hostFixture.ConfigureServicesCallback = ConfigureServices;
                hostFixture.ConfigureApplicationCallback = ConfigureApplication;
                hostFixture.ConfigureHost(this);
            }
            Host = hostFixture.Host;
            ServiceProvider = hostFixture.Host.Services;
            Application = hostFixture.Application;
            Configure(hostFixture.Configuration, hostFixture.HostingEnvironment);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            
        }

        public override void ConfigureApplication(IApplicationBuilder app)
        {
            
        }
    }
}
