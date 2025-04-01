using System;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Cuemon.AspNetCore.Diagnostics;
using Cuemon.Extensions.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    public class WebHostTestFactoryTest : Test
    {
        public WebHostTestFactoryTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Create_ShouldThrowSecurityException_DueToBlockingAspNetCoreHostFixture()
        {
            Assert.Throws<SecurityException>(() => WebHostTestFactory.Create(
                services =>
                {
                    services.AddXunitTestLogging(TestOutput);
                    services.AddAuthorization(o =>
                    {
                        o.AddPolicy("Test", _ => throw new SecurityException());
                    });
                },
                app =>
                {
                    var policy = app.ApplicationServices.GetRequiredService<IAuthorizationPolicyProvider>();
                },
                host =>
                {
                    host.UseDefaultServiceProvider(o =>
                    {
                        o.ValidateOnBuild = false;
                        o.ValidateScopes = false;
                    });
                },
                new BlockingAspNetCoreHostFixture()));
        }

        [Fact]
        public void Create_ShouldCaptureSecurityException_DueToNonBlockingAspNetCoreHostFixture()
        {
            using (var startup = WebHostTestFactory.Create(
                       services =>
                       {
                           services.AddXunitTestLogging(TestOutput);

                           services.AddAuthorization(o => { o.AddPolicy("Test", _ => throw new SecurityException()); });
                       },
                       app =>
                       {
                           var policy = app.ApplicationServices.GetRequiredService<IAuthorizationPolicyProvider>();
                       },
                       host =>
                       {
                           host.UseDefaultServiceProvider(o =>
                           {
                               o.ValidateOnBuild = false;
                               o.ValidateScopes = false;
                           });
                       }))
            {
                var loggerStore = startup.ServiceProvider.GetRequiredService<ILogger<WebHostTestFactoryTest>>().GetTestStore(null);
                var message = loggerStore.Query(entry => entry.Severity == LogLevel.Critical && entry.Message.Contains("SecurityException", StringComparison.OrdinalIgnoreCase)).SingleOrDefault()?.Message;
                Assert.NotNull(message);
                Assert.Contains("System.Security.SecurityException: Security error.", message);
            }
        }

        [Fact]
        public void Create_CallerTypeShouldHaveDeclaringTypeOfMiddlewareTestFactoryTest()
        {
            Type sut1 = GetType();
            string sut2 = null;
            var middleware = WebHostTestFactory.Create(Assert.NotNull, Assert.NotNull, host =>
              {
                  host.ConfigureAppConfiguration((context, _) =>
                  {
                      sut2 = context.HostingEnvironment.ApplicationName;
                  });
              });

            Assert.True(sut1 == middleware.CallerType.DeclaringType);
            Assert.Equal(GetType().Assembly.GetName().Name, sut2);
        }

        [Fact]
        public Task RunAsync_ShouldHaveApplicationNameEqualToThisAssembly()
        {
            return WebHostTestFactory.RunAsync(Assert.NotNull, Assert.NotNull, host =>
              {
                  host.ConfigureAppConfiguration((context, _) =>
                  {
                      TestOutput.WriteLine(context.HostingEnvironment.ApplicationName);
                      Assert.Equal(GetType().Assembly.GetName().Name, context.HostingEnvironment.ApplicationName);
                  });
              }, hostFixture: null);
        }

        [Fact]
        public Task RunWithHostBuilderContextAsync_ShouldHaveApplicationNameEqualToThisAssembly_WithHostBuilderContext()
        {
            return WebHostTestFactory.RunWithHostBuilderContextAsync((context, app) =>
                {
                    Assert.NotNull(context);
                    Assert.NotNull(context.HostingEnvironment);
                    Assert.NotNull(context.Configuration);
                    Assert.NotNull(context.Properties);
                    Assert.NotNull(app);
                },
                (context, services) =>
                {
                    Assert.NotNull(context);
                    Assert.NotNull(context.HostingEnvironment);
                    Assert.NotNull(context.Configuration);
                    Assert.NotNull(context.Properties);
                    Assert.NotNull(services);
                },
                host =>
                {
                    host.ConfigureAppConfiguration((context, configuration) =>
                    {
                        TestOutput.WriteLine(context.HostingEnvironment.ApplicationName);
                        Assert.Equal(GetType().Assembly.GetName().Name, context.HostingEnvironment.ApplicationName);
                    });
                },
                hostFixture: null);
        }

        [Fact]
        public async Task RunAsync_ShouldWorkWithXunitTestLogging()
        {
            using var response = await WebHostTestFactory.RunAsync(
                services =>
                {
                    services.AddXunitTestLogging(TestOutput);
                    services.AddServerTiming(o => o.SuppressHeaderPredicate = _ => false);
                },
                app =>
                {
                    app.UseServerTiming();
                    app.Use(async (context, next) =>
                    {
                        var sw = Stopwatch.StartNew();
                        context.Response.OnStarting(() =>
                        {
                            sw.Stop();
                            context.RequestServices.GetRequiredService<IServerTiming>().AddServerTiming("use-middleware", sw.Elapsed);
                            return Task.CompletedTask;
                        });
                        await next(context).ConfigureAwait(false);
                    });
                    app.Run(context =>
                    {
                        Thread.Sleep(400);
                        return context.Response.WriteAsync("Hello World!");
                    });
                },
                hostFixture: null).ConfigureAwait(false);

            Assert.StartsWith("use-middleware;dur=", response.Headers.Single(kvp => kvp.Key == ServerTiming.HeaderName).Value.FirstOrDefault());
        }
    }
}
