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

internal static class RunExample
{
    static void Main()
    {
        new ResponseFeatureExample().Demonstrate();
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="08b911ef79f0b743282e37c9"><a href="~/run-actions/08b911ef79f0b743282e37c9/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/08b911ef79f0b743282e37c9/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
