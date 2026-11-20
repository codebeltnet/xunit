---
uid: Codebelt.Extensions.Xunit.Hosting.GenericHostFixtureExtensions
example:
- *content
---

The following example validates that an `IGenericHostFixture` has been fully configured by calling `HasValidState` on the fixture. The method returns `true` only when both `ConfigureServicesCallback` and `ConfigureHostCallback` are non-null.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Xunit;

namespace HostFixtureTests;

public class HostStateGuard
{
    public bool EnsureFixtureIsReady(IGenericHostFixture fixture)
    {
        return fixture.HasValidState();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        using var fixture = new ManagedHostFixture();
        Assert.False(new HostStateGuard().EnsureFixtureIsReady(fixture));
        using var app = HostTestFactory.Create(hostFixture: fixture);
        Assert.True(new HostStateGuard().EnsureFixtureIsReady(fixture));
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="1130af506703020850fdadea"><a href="~/run-actions/1130af506703020850fdadea/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/1130af506703020850fdadea/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
