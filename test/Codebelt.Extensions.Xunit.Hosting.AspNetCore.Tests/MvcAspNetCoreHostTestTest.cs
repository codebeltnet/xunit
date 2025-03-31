using System;
using System.Net.Http;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Assets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    public class MvcAspNetCoreHostTestTest : AspNetCoreHostTest<AspNetCoreHostFixture>
    {
        private readonly AspNetCoreHostFixture _hostFixture;
        private readonly HttpClient _client;

        public MvcAspNetCoreHostTestTest(AspNetCoreHostFixture hostFixture, ITestOutputHelper output = null, Type callerType = null) : base(hostFixture, output, callerType)
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

            hostFixture.ServiceProvider.GetRequiredService<ITestOutputHelperAccessor>().TestOutput = output;
            _hostFixture = hostFixture;
            _client = hostFixture.Host.GetTestClient();
        }

        [Fact]
        public async Task GetTestAsync()
        {
            var response = await _client.GetAsync("/Fake");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.Equal("Unit Test", body);
        }

        [Fact]
        public async Task GetTestAsync2()
        {
            var response = await _client.GetAsync("/Fake");
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            Assert.Equal("Unit Test", body);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(FakeController).Assembly);

            services.AddXunitTestLoggingOutputHelperAccessor();
            services.AddXunitTestLogging(TestOutput);
        }

        public override void ConfigureApplication(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(routes => { routes.MapControllers(); });
        }
    }
}
