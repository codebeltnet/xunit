---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.SelfManagedWebMinimalHostFixture
example:
- *content
---

Use `SelfManagedWebMinimalHostFixture` when a minimal test pipeline needs configuration or observation before startup. The example registers endpoint state, creates the pipeline without starting it, then explicitly starts the host and verifies the response through `TestServer`.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DeferredHealthEndpoint.Tests;

public sealed class SelfManagedWebMinimalHostFixtureExample
{
    public async Task<string> StartHealthEndpointWhenReadyAsync()
    {
        using var host = MinimalWebHostTestFactory.Create(
            services => services.AddSingleton(new HealthState("healthy")),
            pipelineSetup: app => app.Run(context =>
            {
                var health = context.RequestServices.GetRequiredService<HealthState>();
                return context.Response.WriteAsync(health.Value);
            }),
            hostFixture: new SelfManagedWebMinimalHostFixture());

        await host.Host.StartAsync().ConfigureAwait(false);
        using var client = host.Host.GetTestClient();

        return await client.GetStringAsync("/").ConfigureAwait(false);
    }
}

public sealed record HealthState(string Value);
```
