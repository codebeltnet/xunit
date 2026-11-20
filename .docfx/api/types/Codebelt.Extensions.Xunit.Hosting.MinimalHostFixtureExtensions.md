---
uid: Codebelt.Extensions.Xunit.Hosting.MinimalHostFixtureExtensions
example:
- *content
---

The following example validates that an `IMinimalHostFixture` has been fully initialized by calling `HasValidState` on the fixture. The method checks that the host, configure-host callback, and configure callback are all non-null.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Xunit;

namespace HostFixtureTests;

public class HostStateGuard
{
    public bool EnsureFixtureIsReady(IMinimalHostFixture fixture)
    {
        return fixture.HasValidState();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        using var fixture = new ManagedMinimalHostFixture();
        Assert.False(new HostStateGuard().EnsureFixtureIsReady(fixture));
        using var app = MinimalHostTestFactory.Create(hostFixture: fixture);
        Assert.True(new HostStateGuard().EnsureFixtureIsReady(fixture));
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="becb2fe7446fc1571dc86953"><a href="~/run-actions/becb2fe7446fc1571dc86953/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/becb2fe7446fc1571dc86953/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
