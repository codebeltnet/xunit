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
```
