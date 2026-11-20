---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features.FakeHttpRequestFeature
example:
- *content
---

The following example creates a `FakeHttpRequestFeature` to simulate the HTTP request metadata in a unit test. The feature provides settable properties for the HTTP method, path, query string, and request body, allowing complete control over the request characteristics.

```csharp
using System;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features;

namespace WebFixtureTests;

public class RequestFeatureExample
{
    public void Demonstrate()
    {
        var request = new FakeHttpRequestFeature();
        Console.WriteLine(request.Method);
    }
}

internal static class RunExample
{
    static void Main()
    {
        new RequestFeatureExample().Demonstrate();
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="f5ed7c321a52580d7ce11408"><a href="~/run-actions/f5ed7c321a52580d7ce11408/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/f5ed7c321a52580d7ce11408/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
