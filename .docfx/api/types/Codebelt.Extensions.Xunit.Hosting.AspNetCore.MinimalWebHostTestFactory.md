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
```
