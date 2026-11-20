---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.HttpClientExtensions
example:
- *content
---

The following example sends an HTTP request using the `ToHttpResponseMessageAsync` extension on an `HttpClient`. By default it performs a GET request to the root URL ("/"), or you can supply a custom response factory delegate. This method is designed to work with test host fixtures that provide a pre-configured `HttpClient`.

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;

namespace WebFixtureTests;

public class HttpClientExample
{
    public async Task DemonstrateAsync(HttpClient client)
    {
        var response = await client.ToHttpResponseMessageAsync().ConfigureAwait(false);
        Console.WriteLine(response.StatusCode);
    }
}

internal static class RunExample
{
    static async Task Main()
    {
        using var app = WebHostTestFactory.Create(pipelineSetup: app => app.Run(context =>
        {
            context.Response.StatusCode = 202;
            return Task.CompletedTask;
        }));
        using var client = app.Host.GetTestClient();
        await new HttpClientExample().DemonstrateAsync(client);
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="947406357e3e26b26a71a538"><a href="~/run-actions/947406357e3e26b26a71a538/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/947406357e3e26b26a71a538/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
