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
using Xunit;

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

public sealed class RunExample
{
    [Fact]
    public async Task ExercisesExample()
    {
        Assert.Equal("ready", await new WebHostTestFactoryExample().InvokeStatusMiddlewareAsync());
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="d71e8ed85f1221cb41308b31"><a href="~/run-actions/d71e8ed85f1221cb41308b31/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/d71e8ed85f1221cb41308b31/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
