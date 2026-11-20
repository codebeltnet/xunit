---
uid: Codebelt.Extensions.Xunit.Hosting.MinimalHostTestFactory
example:
- *content
---

The following example uses `MinimalHostTestFactory` to create a lightweight host context for testing a simple service. Unlike the generic host factory, the minimal host avoids the full `IHostBuilder` pipeline and is suitable for isolated unit tests that only need dependency injection and logging.

```csharp
using System;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HostFixtureTests;

public class MinimalHostFactoryExample
{
    public void Demonstrate()
    {
        using var host = MinimalHostTestFactory.Create(
            services => services.AddSingleton(new Version(1, 0, 0)));
        var version = host.Host.Services.GetRequiredService<Version>();
        Console.WriteLine(version);
    }
}

internal static class RunExample
{
    static void Main()
    {
        new MinimalHostFactoryExample().Demonstrate();
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="e3f79d1c0f2fa85276cee2fb"><a href="~/run-actions/e3f79d1c0f2fa85276cee2fb/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/e3f79d1c0f2fa85276cee2fb/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
