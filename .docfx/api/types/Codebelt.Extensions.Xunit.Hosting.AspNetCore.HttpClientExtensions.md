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

namespace WebFixtureTests;

public class HttpClientExample
{
    public async Task DemonstrateAsync(HttpClient client)
    {
        var response = await client.ToHttpResponseMessageAsync().ConfigureAwait(false);
        Console.WriteLine(response.StatusCode);
    }
}
```
