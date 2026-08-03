---
uid: Codebelt.Extensions.Xunit.Hosting.ApplicationHostFactory
example:
- *content
---

The test project references a worker application's entry-point assembly. `ApplicationHostFactory.Create<TEntryPoint>` preserves the current minor-release compatibility path, including direct use of an application's `CreateHostBuilder` when it is available. When the application entry point should own startup, pass `ManagedApplicationFixture<TEntryPoint>` to `ApplicationTestFactory.Create<TEntryPoint>`; the fixture opts into the deferred path without changing the existing factory method signature. The compatibility path is intentionally retained until it can be removed or changed in the next major release. Because this lower-level factory returns the host directly, the caller still owns disposal.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerApp.Tests;

public sealed class ApplicationHostFactoryExample
{
    public string GetTestIdentity()
    {
        using IHost host = ApplicationHostFactory.Create<WorkerProgram>(builder =>
        {
            builder.ConfigureServices(services =>
                services.AddSingleton(new WorkerIdentity("Test inventory worker")));
        });

        var identity = host.Services.GetRequiredService<WorkerIdentity>();
        return identity.Name;
    }
}

public sealed record WorkerIdentity(string Name);

public sealed class WorkerProgram
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args);
    }

    public static void Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        host.Run();
    }
}
```
