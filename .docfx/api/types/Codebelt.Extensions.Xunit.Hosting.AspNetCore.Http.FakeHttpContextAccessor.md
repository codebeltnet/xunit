---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.FakeHttpContextAccessor
example:
- *content
---

The following example creates a `FakeHttpContextAccessor` instance for unit testing code that depends on `IHttpContextAccessor`. The fake initializes a `DefaultHttpContext` with `FakeHttpRequestFeature` and `FakeHttpResponseFeature`, providing a complete HTTP context without running a real server.

```csharp
using System;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http;

namespace WebFixtureTests;

public class HttpContextExample
{
    public void Demonstrate()
    {
        var accessor = new FakeHttpContextAccessor();
        Console.WriteLine(accessor.HttpContext.Request.Method);
    }
}
```
