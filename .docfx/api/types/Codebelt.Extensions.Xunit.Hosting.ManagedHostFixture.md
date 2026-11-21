---
uid: Codebelt.Extensions.Xunit.Hosting.ManagedHostFixture
example:
- *content
---

The following example uses `ManagedHostFixture` as the fixture type for a generic host test class. The fixture owns the full `IHost` lifecycle, starting the host before the test runs and disposing it afterward. This is the standard pattern for testing services that depend on dependency injection and need a running host.

```csharp
using System.Threading;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace HostFixtureTests;

public class BackgroundServiceTest : HostTest<ManagedHostFixture>
{
    public BackgroundServiceTest(ManagedHostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddHostedService<TestWorker>();
        services.AddXunitTestLogging();
    }

    [Fact]
    public void Host_ShouldBeRunning()
    {
        Assert.NotNull(Host);
    }
}

public class TestWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(100, stoppingToken);
        }
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="aa478d74f298d8c98e6cd1bf"><a href="~/run-actions/aa478d74f298d8c98e6cd1bf/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/aa478d74f298d8c98e6cd1bf/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
