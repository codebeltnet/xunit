using System; 
using System.Diagnostics; 
using System.Linq; 
using System.Threading; 
using System.Threading.Tasks; 
using Cuemon.AspNetCore.Diagnostics; 
using Cuemon.Extensions.AspNetCore.Diagnostics; 
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Http; 
using Microsoft.Extensions.DependencyInjection; 
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
              }); 
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
                }); 
        } 
 
        [Fact] 
        public async Task RunAsync_ShouldWorkWithXunitTestLogging() 
        { 
            using var response = await WebHostTestFactory.RunAsync( 
                services => 
                { 
                    services.AddXunitTestLogging(TestOutput); 
                    services.AddServerTiming(o => o.SuppressHeaderPredicate = _ => false); 
                } 
                , app => 
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
                }).ConfigureAwait(false); 
 
            Assert.StartsWith("use-middleware;dur=", response.Headers.Single(kvp => kvp.Key == ServerTiming.HeaderName).Value.FirstOrDefault()); 
        } 
    } 
} 
