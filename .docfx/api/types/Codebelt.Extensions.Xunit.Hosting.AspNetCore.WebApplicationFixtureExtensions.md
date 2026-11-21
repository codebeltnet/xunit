---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebApplicationFixtureExtensions
example:
- *content
---

The following example validates that an `IWebApplicationFixture<TEntryPoint>` has a valid initialized state by calling `HasValidState<TEntryPoint>`. The method checks that the host, configure callback, and configure-web-host callback are all non-null.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Xunit;

namespace WebFixtureTests;

public class WebAppStateGuard
{
    public bool EnsureFixtureIsReady<TEntryPoint>(IWebApplicationFixture<TEntryPoint> fixture) where TEntryPoint : class
    {
        return fixture.HasValidState<TEntryPoint>();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        using var fixture = new ManagedWebApplicationFixture<RunExample>();
        Assert.False(new WebAppStateGuard().EnsureFixtureIsReady(fixture));
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="549c3e82bd0bcb99e9f05bfa"><a href="~/run-actions/549c3e82bd0bcb99e9f05bfa/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/549c3e82bd0bcb99e9f05bfa/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
