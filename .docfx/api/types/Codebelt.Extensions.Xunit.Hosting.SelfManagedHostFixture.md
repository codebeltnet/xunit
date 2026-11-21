---
uid: Codebelt.Extensions.Xunit.Hosting.SelfManagedHostFixture
example:
- *content
---

Use `SelfManagedHostFixture` when the host must be built before it is started. The example attaches an `ApplicationStarted` observer first, verifies that factory creation did not start the host, and then starts it explicitly so the lifecycle transition is under test control.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace WorkerLifecycle.Tests;

public sealed class SelfManagedHostFixtureExample
{
    public async Task StartAfterAttachingObserverAsync()
    {
        using var host = HostTestFactory.Create(
            hostFixture: new SelfManagedHostFixture());

        var started = false;
        var lifetime = host.Host.Services.GetRequiredService<IHostApplicationLifetime>();
        using var registration = lifetime.ApplicationStarted.Register(() => started = true);

        Assert.False(started);
        await host.Host.StartAsync().ConfigureAwait(false);
        Assert.True(started);
    }
}

public sealed class RunExample
{
    [Fact]
    public async Task ExercisesExample()
    {
        await new SelfManagedHostFixtureExample().StartAfterAttachingObserverAsync();
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="515575be5bb7683cac9903a5"><a href="~/run-actions/515575be5bb7683cac9903a5/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/515575be5bb7683cac9903a5/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
