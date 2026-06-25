---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.ManagedWebHostFixture
example:
- *content
---

The following example uses `ManagedWebHostFixture` as the fixture type for a web host test class. The fixture owns the full `IWebHost` lifecycle, starting the web host before tests and disposing it after, making it suitable for integration testing of middleware, controllers, and Razor Pages.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace WebFixtureTests;

public class MiddlewareTest : WebHostTest<ManagedWebHostFixture>
{
    public MiddlewareTest(ManagedWebHostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddFakeHttpContextAccessor();
    }

    public override void ConfigureApplication(IApplicationBuilder app)
    {
        app.UseMiddleware<TestMiddleware>();
    }

    [Fact]
    public async Task Middleware_ShouldAddHeader()
    {
        var pipeline = Application.Build();
        var context = new DefaultHttpContext();
        await pipeline(context);
        Assert.True(context.Response.Headers.ContainsKey("X-Test"));
    }
}

public class TestMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.Headers["X-Test"] = "true";
        await next(context);
    }
}
```
