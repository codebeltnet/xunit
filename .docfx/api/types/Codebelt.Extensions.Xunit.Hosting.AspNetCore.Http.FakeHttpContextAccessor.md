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

internal static class RunExample
{
    static void Main()
    {
        new HttpContextExample().Demonstrate();
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="3a1dbfc0e010519636b7091e"><a href="~/run-actions/3a1dbfc0e010519636b7091e/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/3a1dbfc0e010519636b7091e/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
