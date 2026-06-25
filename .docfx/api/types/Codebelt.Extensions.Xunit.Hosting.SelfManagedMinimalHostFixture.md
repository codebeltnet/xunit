---
uid: Codebelt.Extensions.Xunit.Hosting.SelfManagedMinimalHostFixture
example:
- *content
---

Use `SelfManagedMinimalHostFixture` when a minimal host should be configured and inspected before startup. This example verifies a service registered through `MinimalHostTestFactory`, starts the host only after the pre-start assertion, and confirms the same service remains available from the running host.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MinimalWorkerLifecycle.Tests;

public sealed class SelfManagedMinimalHostFixtureExample
{
    public async Task ConfigureBeforeStartingAsync()
    {
        using var host = MinimalHostTestFactory.Create(
            services => services.AddSingleton(new StartupState("configured")),
            hostFixture: new SelfManagedMinimalHostFixture());

        var beforeStart = host.Host.Services.GetRequiredService<StartupState>();
        Assert.Equal("configured", beforeStart.Value);

        await host.Host.StartAsync().ConfigureAwait(false);

        var afterStart = host.Host.Services.GetRequiredService<StartupState>();
        Assert.Same(beforeStart, afterStart);
    }
}

public sealed record StartupState(string Value);
```
