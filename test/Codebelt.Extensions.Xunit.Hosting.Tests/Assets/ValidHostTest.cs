using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace Codebelt.Extensions.Xunit.Hosting.Assets
{
    public class ValidHostTest : HostTest<HostFixture>
    {
        public ValidHostTest(HostFixture hostFixture) : base(hostFixture)
        {
            if (!hostFixture.HasValidState())
            {
                hostFixture.ConfigureHostCallback = ConfigureHost;
                hostFixture.ConfigureCallback = Configure;
                hostFixture.ConfigureServicesCallback = ConfigureServices;
                hostFixture.ConfigureHost(this);
            }
            Host = hostFixture.Host;
            ServiceProvider = hostFixture.Host.Services;
            Configure(hostFixture.Configuration, hostFixture.HostingEnvironment);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}
