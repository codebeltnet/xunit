---
uid: Codebelt.Extensions.Xunit.Hosting.ApplicationFixtureExtensions
example:
- *content
---

The following example validates that an `IApplicationFixture<TEntryPoint>` has been fully initialized with host, configure callback, and configure-host callback by calling `HasValidState<TEntryPoint>` on the fixture. This is useful in assertion helpers to confirm the fixture was set up correctly before running the test scenario.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Xunit;

namespace HostFixtureTests;

public class HostStateGuard
{
    public bool EnsureFixtureIsReady<TEntryPoint>(IApplicationFixture<TEntryPoint> fixture) where TEntryPoint : class
    {
        return fixture.HasValidState<TEntryPoint>();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        using var fixture = new ManagedApplicationFixture<RunExample>();
        Assert.False(new HostStateGuard().EnsureFixtureIsReady(fixture));
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="e7dbb5ba733b205469239b03"><a href="~/run-actions/e7dbb5ba733b205469239b03/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/e7dbb5ba733b205469239b03/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
