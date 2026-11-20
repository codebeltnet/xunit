---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.ManagedWebMinimalHostFixture
example:
- *content
---

The following example uses `ManagedWebMinimalHostFixture` as the fixture type for a minimal web host test class. The fixture starts and manages a minimal web host, giving the test access to a configured host and the ability to send HTTP requests through the ASP.NET Core pipeline.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace WebFixtureTests;

public class MinimalApiTest : MinimalWebHostTest<ManagedWebMinimalHostFixture>
{
    public MinimalApiTest(ManagedWebMinimalHostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    protected override void ConfigureHost(IHostApplicationBuilder hb)
    {
        hb.Services.AddFakeHttpContextAccessor();
    }

    public override void ConfigureApplication(IApplicationBuilder app)
    {
        app.UseMiddleware<TestMiddleware>();
    }
}

public class TestMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers["X-Test"] = "true";
        await next(context);
    }
}

public sealed class RunExample(ManagedWebMinimalHostFixture fixture, ITestOutputHelper output) : MinimalApiTest(fixture, output)
{
    [Fact]
    public async Task BuildsAndInvokesTheConfiguredPipeline()
    {
        var context = new DefaultHttpContext
        {
            RequestServices = Application.ApplicationServices
        };
        await Application.Build()(context);
        Assert.Equal("true", context.Response.Headers["X-Test"].ToString());
        Assert.NotNull(context.RequestServices.GetRequiredService<IHttpContextAccessor>());
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="00acdae546dce87b654496e1"><a href="~/run-actions/00acdae546dce87b654496e1/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/00acdae546dce87b654496e1/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
