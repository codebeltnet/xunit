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
using Xunit;

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

public sealed class RunExample
{
    [Fact]
    public async Task ExercisesExample()
    {
        Assert.Equal("healthy", await new SelfManagedWebMinimalHostFixtureExample().StartHealthEndpointWhenReadyAsync());
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="3c3b98167b30d6985017ec27"><a href="~/run-actions/3c3b98167b30d6985017ec27/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/3c3b98167b30d6985017ec27/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
