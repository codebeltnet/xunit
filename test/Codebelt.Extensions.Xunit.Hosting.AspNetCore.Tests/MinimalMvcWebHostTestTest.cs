using System;
using System.Net.Http;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Assets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    public class MinimalMvcWebHostTestTest : MinimalWebHostTest<ManagedWebMinimalHostFixture>
    {
        private readonly ManagedWebMinimalHostFixture _hostFixture;
        private readonly HttpClient _client;

        public MinimalMvcWebHostTestTest(ManagedWebMinimalHostFixture hostFixture, ITestOutputHelper output = null, Type callerType = null) : base(hostFixture, output, callerType)
        {
            hostFixture.Host.Services.GetRequiredService<ITestOutputHelperAccessor>().TestOutput = output;
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

        protected override void ConfigureHost(IHostApplicationBuilder hb)
        {
            hb.Services.AddControllers()
                .AddApplicationPart(typeof(FakeController).Assembly);

            hb.Services.AddXunitTestLoggingOutputHelperAccessor();
            hb.Services.AddXunitTestLogging(TestOutput);
        }

        public override void ConfigureApplication(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(routes => { routes.MapControllers(); });
        }
    }
}
