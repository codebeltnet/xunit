---
uid: Codebelt.Extensions.Xunit.Hosting.BlockingManagedApplicationFixture`1
example:
- *content
---

The test project references a worker application's entry point and shares one bootstrapped host through xUnit's class-fixture lifetime. `BlockingManagedApplicationFixture<TEntryPoint>` waits until the host is ready before constructing the test class, so each test can immediately resolve services registered by the real application.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace InventoryWorker.Tests;

public sealed class InventoryWorkerTest : IClassFixture<BlockingManagedApplicationFixture<WorkerProgram>>
{
    private readonly BlockingManagedApplicationFixture<WorkerProgram> _fixture;

    public InventoryWorkerTest(BlockingManagedApplicationFixture<WorkerProgram> fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void Host_ContainsApplicationService()
    {
        var identity = _fixture.Host.Services.GetRequiredService<WorkerIdentity>();

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
