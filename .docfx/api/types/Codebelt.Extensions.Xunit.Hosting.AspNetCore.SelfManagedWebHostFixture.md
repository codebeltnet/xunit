---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.SelfManagedWebHostFixture
example:
- *content
---

Use `SelfManagedWebHostFixture` when a conventional test pipeline must exist before the server starts. The example builds middleware, explicitly starts the host, and then sends a request through `TestServer`, making the startup boundary and resulting response visible.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace DeferredPipeline.Tests;

public sealed class SelfManagedWebHostFixtureExample
{
    public async Task<string> StartPipelineWhenReadyAsync()
    {
        using var host = WebHostTestFactory.Create(
            pipelineSetup: app => app.Run(context =>
                context.Response.WriteAsync("started on demand")),
            hostFixture: new SelfManagedWebHostFixture());

        await host.Host.StartAsync().ConfigureAwait(false);
        using var client = host.Host.GetTestClient();

        return await client.GetStringAsync("/").ConfigureAwait(false);
    }
}
```
