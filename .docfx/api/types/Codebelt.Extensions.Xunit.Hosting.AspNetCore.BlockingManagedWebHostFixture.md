---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.BlockingManagedWebHostFixture
example:
- *content
---

The following example passes a `BlockingManagedWebHostFixture` to `WebHostTestFactory.Create` when a test needs a synchronously started web host. The factory configures the host and returns control after startup, so the test can check `ApplicationStarted` immediately.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace WebFixtureTests;

public class WebHostIntegrationTest
{
    [Fact]
    public void HostShouldBeStarted()
    {
        using var fixture = new BlockingManagedWebHostFixture();
        using var app = WebHostTestFactory.Create(hostFixture: fixture);

        Assert.True(fixture.HasValidState());
        var lifetime = app.Host.Services.GetRequiredService<IHostApplicationLifetime>();
        Assert.True(lifetime.ApplicationStarted.IsCancellationRequested);
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="21b422ce67f920d801901db0"><a href="~/run-actions/21b422ce67f920d801901db0/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/21b422ce67f920d801901db0/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->

The [maintained fixture test](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.Tests/BlockingManagedWebHostFixtureTest.cs) shows the basic factory setup in the repository’s test suite.
