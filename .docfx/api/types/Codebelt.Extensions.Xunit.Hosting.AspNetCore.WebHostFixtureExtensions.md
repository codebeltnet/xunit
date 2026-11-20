---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebHostFixtureExtensions
example:
- *content
---

The following example validates that an `IWebHostFixture` has a valid initialized state by calling `HasValidState`. The method checks that the underlying generic host fixture is valid and that the application configure callback is set.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Xunit;

namespace WebFixtureTests;

public class WebHostStateGuard
{
    public bool EnsureFixtureIsReady(IWebHostFixture fixture)
    {
        return fixture.HasValidState();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        using var fixture = new ManagedWebHostFixture();
        Assert.False(new WebHostStateGuard().EnsureFixtureIsReady(fixture));
        using var app = WebHostTestFactory.Create(hostFixture: fixture);
        Assert.True(new WebHostStateGuard().EnsureFixtureIsReady(fixture));
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="dfdd064179bf4643e1a0b2bf"><a href="~/run-actions/dfdd064179bf4643e1a0b2bf/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/dfdd064179bf4643e1a0b2bf/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
