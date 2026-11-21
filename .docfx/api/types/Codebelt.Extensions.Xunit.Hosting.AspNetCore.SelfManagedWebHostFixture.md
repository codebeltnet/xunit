---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.SelfManagedWebHostFixture
example:
- *content
---

Use `SelfManagedWebHostFixture` when a conventional test pipeline must exist before the server starts. The example builds middleware, explicitly starts the host, and then sends a request through `TestServer`, making the startup boundary and resulting response visible.

```csharp
// Program.cs
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var host = WebHostTestFactory.Create(
            pipelineSetup: app => app.Run(context =>
                context.Response.WriteAsync("started on demand")),
            hostFixture: new SelfManagedWebHostFixture());

        await host.Host.StartAsync().ConfigureAwait(false);
        using var client = host.Host.GetTestClient();

        Console.WriteLine(await client.GetStringAsync("/").ConfigureAwait(false));
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="f27f77d838407171b56f9073"><a href="~/run-actions/f27f77d838407171b56f9073/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/f27f77d838407171b56f9073/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
