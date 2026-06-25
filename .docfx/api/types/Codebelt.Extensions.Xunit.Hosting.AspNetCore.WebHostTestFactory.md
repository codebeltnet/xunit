---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebHostTestFactory
example:
- *content
---

Use `WebHostTestFactory` when the test defines a conventional ASP.NET Core service collection and middleware pipeline instead of bootstrapping an application entry point. `RunAsync` starts that pipeline on `TestServer`, sends a request, and returns the response so the caller can verify middleware behavior.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RequestPipeline.Tests;

public sealed class WebHostTestFactoryExample
{
    public async Task<string> InvokeStatusMiddlewareAsync()
    {
        using var response = await WebHostTestFactory.RunAsync(
            services => services.AddSingleton(new PipelineStatus("ready")),
            app => app.Run(context =>
            {
                var status = context.RequestServices.GetRequiredService<PipelineStatus>();
                return context.Response.WriteAsync(status.Value);
            })).ConfigureAwait(false);

        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }
}

public sealed record PipelineStatus(string Value);
```
