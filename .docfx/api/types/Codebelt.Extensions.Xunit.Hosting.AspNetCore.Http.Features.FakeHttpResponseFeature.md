---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features.FakeHttpResponseFeature
example:
- *content
---

The following example creates a `FakeHttpResponseFeature` to simulate the HTTP response surface in a unit test. The feature provides settable properties for the status code, reason phrase, headers, and response body, allowing the test to inspect what the code under test writes to the response.

```csharp
using System;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features;

namespace WebFixtureTests;

public class ResponseFeatureExample
{
    public void Demonstrate()
    {
        var response = new FakeHttpResponseFeature();
        Console.WriteLine(response.StatusCode);
    }
}
```
