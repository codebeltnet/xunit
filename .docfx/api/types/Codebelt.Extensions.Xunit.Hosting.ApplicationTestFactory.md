---
uid: Codebelt.Extensions.Xunit.Hosting.ApplicationTestFactory
example:
- *content
---

The test project references a worker application whose entry point registers `WorkerIdentity`. `ApplicationTestFactory` uses `ManagedApplicationFixture<TEntryPoint>` by default, runs that application's real host setup, and exposes its services and environment through an owned test context so the test can verify application behavior without recreating `Program` configuration. Pass an explicit `IApplicationFixture<TEntryPoint>` when the test needs a different lifecycle.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace WorkerApp.Tests;

public sealed class ApplicationTestFactoryExample
{
    [Fact]
    public void Create_BootstrapsWorkerApplicationServices()
    {
        using var application = ApplicationTestFactory.Create<WorkerProgram>();

        var identity = application.Host.Services.GetRequiredService<WorkerIdentity>();

        Assert.Equal("Inventory worker", identity.Name);
        Assert.Equal(Environments.Development, application.Environment.EnvironmentName);
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
