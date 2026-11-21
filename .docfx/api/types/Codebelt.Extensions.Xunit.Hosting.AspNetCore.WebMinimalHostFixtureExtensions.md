---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebMinimalHostFixtureExtensions
example:
- *content
---

The following example validates that an `IWebMinimalHostFixture` has a valid initialized state by calling `HasValidState`. The method checks the minimal host fixture state and additionally verifies that the application callback and application pipeline are configured.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Xunit;

namespace WebFixtureTests;

public class WebMinimalHostStateGuard
{
    public bool EnsureFixtureIsReady(IWebMinimalHostFixture fixture)
    {
        return fixture.HasValidState();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        using var fixture = new ManagedWebMinimalHostFixture();
        Assert.False(new WebMinimalHostStateGuard().EnsureFixtureIsReady(fixture));
        using var app = MinimalWebHostTestFactory.Create(hostFixture: fixture);
        Assert.True(new WebMinimalHostStateGuard().EnsureFixtureIsReady(fixture));
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="dd2fa1a038dbc1220b366a53"><a href="~/run-actions/dd2fa1a038dbc1220b366a53/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/dd2fa1a038dbc1220b366a53/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
