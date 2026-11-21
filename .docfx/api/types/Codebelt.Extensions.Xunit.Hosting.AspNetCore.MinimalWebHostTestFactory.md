---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.MinimalWebHostTestFactory
example:
- *content
---

Use `MinimalWebHostTestFactory` when a test needs the modern `IHostApplicationBuilder` configuration model and a small in-memory request pipeline. The example supplies a health state through dependency injection, serves it from middleware, and reads the response returned by `RunAsync`.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HealthEndpoint.Tests;

public sealed class MinimalWebHostTestFactoryExample
{
    public async Task<string> ReadHealthStateAsync()
    {
        using var response = await MinimalWebHostTestFactory.RunAsync(
            services => services.AddSingleton(new HealthState("healthy")),
            app => app.Run(context =>
            {
                var health = context.RequestServices.GetRequiredService<HealthState>();
                return context.Response.WriteAsync(health.Value);
            })).ConfigureAwait(false);

        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    }
}

public sealed record HealthState(string Value);

public sealed class RunExample
{
    [Fact]
    public async Task ExercisesExample()
    {
        Assert.Equal("healthy", await new MinimalWebHostTestFactoryExample().ReadHealthStateAsync());
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="1e869fe39c4833386733c21f"><a href="~/run-actions/1e869fe39c4833386733c21f/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/1e869fe39c4833386733c21f/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
