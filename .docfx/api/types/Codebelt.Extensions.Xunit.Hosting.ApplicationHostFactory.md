---
uid: Codebelt.Extensions.Xunit.Hosting.ApplicationHostFactory
example:
- *content
---

The test project references a worker application's entry-point assembly. `ApplicationHostFactory` captures the host built by that entry point and applies a test-only service override; because this lower-level factory returns the host directly, the caller starts, stops, and disposes it explicitly.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerApp.Tests;

public sealed class ApplicationHostFactoryExample
{
    public async Task<string> StartWithTestIdentityAsync()
    {
        using IHost host = ApplicationHostFactory.Create<WorkerProgram>(builder =>
        {
            builder.ConfigureServices(services =>
                services.AddSingleton(new WorkerIdentity("Test inventory worker")));
        });

        await host.StartAsync().ConfigureAwait(false);
        var identity = host.Services.GetRequiredService<WorkerIdentity>();
        await host.StopAsync().ConfigureAwait(false);

        return identity.Name;
    }
}

public sealed record WorkerIdentity(string Name);

public sealed class WorkerProgram
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddSingleton(new WorkerIdentity("Inventory worker"));

        using var host = builder.Build();
        host.Run();
    }
}
```
