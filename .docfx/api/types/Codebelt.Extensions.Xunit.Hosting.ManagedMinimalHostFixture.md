---
uid: Codebelt.Extensions.Xunit.Hosting.ManagedMinimalHostFixture
example:
- *content
---

The following example uses `ManagedMinimalHostFixture` as the fixture type for a minimal host test class. The fixture automatically starts the minimal host and makes it available for the test duration, then disposes it. This is ideal for lean test scenarios that do not need the full generic host.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace HostFixtureTests;

public class MinimalWorkerTest : MinimalHostTest<ManagedMinimalHostFixture>
{
    public MinimalWorkerTest(ManagedMinimalHostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    protected override void ConfigureHost(IHostApplicationBuilder hb)
    {
        hb.Services.AddSingleton<MetricsTracker>();
        hb.Services.AddXunitTestLogging();
    }

    [Fact]
    public void Host_ShouldNotBeNull()
    {
        Assert.NotNull(Host);
    }
}

public class MetricsTracker;
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="6a59b7744a7d9994ffc04782"><a href="~/run-actions/6a59b7744a7d9994ffc04782/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/6a59b7744a7d9994ffc04782/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
