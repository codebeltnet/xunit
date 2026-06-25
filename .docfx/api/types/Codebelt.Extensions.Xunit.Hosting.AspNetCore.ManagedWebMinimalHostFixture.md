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

internal class TestMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);
    }
}
```
