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
```
