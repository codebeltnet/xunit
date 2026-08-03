---
uid: Codebelt.Extensions.Xunit.Hosting.ManagedApplicationFixture`1
example:
- *content
---

Use `ManagedApplicationFixture<TEntryPoint>` when an xUnit class fixture should exercise the application's real entry point and let that entry point start the host. Derive the test from `ApplicationTest<TEntryPoint,T>` and pass the fixture to its base constructor so the base class initializes the fixture through `ConfigureHost` before the test reads `Host`. This is an opt-in path for the current minor release. Fixture setup remains lazy; accessing the test host starts the deferred host and surfaces startup failures at the point the test consumes it. The legacy blocking path is retained for compatibility until it can be removed or changed in the next major release.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace InventoryWorker.Tests;

public sealed class InventoryWorkerTest : ApplicationTest<WorkerProgram, ManagedApplicationFixture<WorkerProgram>>
{
    public InventoryWorkerTest(ManagedApplicationFixture<WorkerProgram> fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public void Host_ContainsApplicationService()
    {
        var identity = Host.Services.GetRequiredService<WorkerIdentity>();

        Assert.Equal("Inventory worker", identity.Name);
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
