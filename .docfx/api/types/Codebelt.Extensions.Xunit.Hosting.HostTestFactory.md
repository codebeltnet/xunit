---
uid: Codebelt.Extensions.Xunit.Hosting.HostTestFactory
example:
- *content
---

Use `HostTestFactory` when the test owns the Generic Host setup instead of loading an existing application entry point. This example registers a worker-facing status service, creates a managed host context, and reads the configured value through the same service provider the hosted application would use.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WorkerServices.Tests;

public sealed class HostTestFactoryExample
{
    public string ResolveWorkerStatus()
    {
        using var host = HostTestFactory.Create(
            services => services.AddSingleton(new WorkerStatus("ready")));

        var status = host.Host.Services.GetRequiredService<WorkerStatus>();
        return status.Value;
    }
}

public sealed record WorkerStatus(string Value);
```
