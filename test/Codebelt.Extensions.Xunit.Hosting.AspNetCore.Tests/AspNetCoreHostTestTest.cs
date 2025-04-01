using System;
using System.Linq;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Assets;
using Cuemon.Extensions.IO;
using Cuemon.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    public class AspNetCoreHostTestTest : AspNetCoreHostTest<AspNetCoreHostFixture>
    {
        private readonly IServiceProvider _provider;
        private readonly IApplicationBuilder _pipeline;

        public AspNetCoreHostTestTest(AspNetCoreHostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
        {
            _pipeline = hostFixture.Application;
            _provider = hostFixture.ServiceProvider;
            _provider.GetRequiredService<ITestOutputHelperAccessor>().TestOutput = output;
        }

        [Fact]
        public async Task ShouldHaveResultOfBoolMiddlewareInBody()
        {
            var context = _provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
            var options = _provider.GetRequiredService<IOptions<BoolOptions>>();
            var pipeline = _pipeline.Build();

            Assert.Equal("Hello awesome developers!", context!.Response.Body.ToEncodedString(o => o.LeaveOpen = true));

            var logger = _pipeline.ApplicationServices.GetRequiredService<ILogger<AspNetCoreHostTestTest>>();
            logger.LogInformation("Hello from {0}", nameof(ShouldHaveResultOfBoolMiddlewareInBody));

            await pipeline(context);

            Assert.Equal("A:True, B:False, C:True, D:False, E:True, F:False", context.Response.Body.ToEncodedString());

            Assert.True(options.Value.A);
            Assert.False(options.Value.B);
            Assert.True(options.Value.C);
            Assert.False(options.Value.D);
            Assert.True(options.Value.E);
            Assert.False(options.Value.F);
        }

#if NET9_0_OR_GREATER
        [Fact]
        public void ShouldThrowInvalidOperationException_BecauseOneOfTheServicesIsScoped()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _provider.GetServices<ICorrelationToken>());

            TestOutput.WriteLine(ex.Message);

            Assert.Contains("from root provider because it requires scoped service", ex.Message);
        }
#endif

        [Fact]
        public void ShouldHaveAccessToCorrelationTokens_UsingScopedProvider()
        {
            using var scope = _provider.CreateScope();

            var firstRequest = scope.ServiceProvider.GetServices<ICorrelationToken>().ToList();
            var secondRequest = scope.ServiceProvider.GetServices<ICorrelationToken>().ToList();

            TestOutput.WriteLine("----");
            TestOutput.WriteLines(firstRequest);
            TestOutput.WriteLine("----");
            TestOutput.WriteLines(secondRequest);

            Assert.Equal(3, firstRequest.Count);
            Assert.Equal(3, secondRequest.Count);

            Assert.Same(firstRequest[0], secondRequest[0]);
            Assert.NotSame(firstRequest[1], secondRequest[1]);
            Assert.Same(firstRequest[2], secondRequest[2]);

            Assert.Equal(firstRequest[0].CorrelationId, secondRequest[0].CorrelationId);
            Assert.NotEqual(firstRequest[1].CorrelationId, secondRequest[1].CorrelationId);
            Assert.Equal(firstRequest[2].CorrelationId, secondRequest[2].CorrelationId);
        }

        [Fact]
        public void ShouldHaveAccessToCorrelationTokens_UsingRequestServices() // reference: https://github.com/dotnet/aspnetcore/blob/main/src/Http/Http/src/Features/RequestServicesFeature.cs
        {
            var context = _provider.GetRequiredService<IHttpContextAccessor>().HttpContext!;

            var firstRequest = context.RequestServices.GetServices<ICorrelationToken>().ToList();
            var secondRequest = context.RequestServices.GetServices<ICorrelationToken>().ToList();

            TestOutput.WriteLine("----");
            TestOutput.WriteLines(firstRequest);
            TestOutput.WriteLine("----");
            TestOutput.WriteLines(secondRequest);

            Assert.Equal(3, firstRequest.Count);
            Assert.Equal(3, secondRequest.Count);

            Assert.Same(firstRequest[0], secondRequest[0]);
            Assert.NotSame(firstRequest[1], secondRequest[1]);
            Assert.Same(firstRequest[2], secondRequest[2]);

            Assert.Equal(firstRequest[0].CorrelationId, secondRequest[0].CorrelationId);
            Assert.NotEqual(firstRequest[1].CorrelationId, secondRequest[1].CorrelationId);
            Assert.Equal(firstRequest[2].CorrelationId, secondRequest[2].CorrelationId);
        }

        [Fact]
        public void ShouldLogToXunitTestLogging()
        {
            var context = _provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
            var logger = _pipeline.ApplicationServices.GetRequiredService<ILogger<AspNetCoreHostTestTest>>();
            logger.LogInformation("Hello from {0}", nameof(ShouldLogToXunitTestLogging));
            var store = _pipeline.ApplicationServices.GetRequiredService<ILogger<AspNetCoreHostTestTest>>().GetTestStore();
            var entry = store.Query(entry => entry.Message.Contains("Hello from", StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

            Assert.NotNull(entry);
            Assert.Equal("Information: Hello from ShouldLogToXunitTestLogging", entry.Message);
        }

        public override void ConfigureApplication(IApplicationBuilder app)
        {
            app.ApplicationServices.GetRequiredService<ILogger<AspNetCoreHostTestTest>>().LogInformation(nameof(ConfigureApplication));
            app.UseMiddleware<BoolMiddleware>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddFakeHttpContextAccessor();
            services.Configure<BoolOptions>(o =>
            {
                o.A = true;
                o.C = true;
                o.E = true;
            });
            services.AddXunitTestLoggingOutputHelperAccessor();
            services.AddXunitTestLogging(TestOutput);

            services.AddSingleton<ICorrelationToken, SingletonCorrelation>();
            services.AddTransient<ICorrelationToken, TransientCorrelation>();
            services.AddScoped<ICorrelationToken, ScopedCorrelation>();
        }
    }
}
