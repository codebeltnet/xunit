---
uid: Codebelt.Extensions.Xunit.Hosting.ManagedApplicationFixture`1
example:
- *content
---

Use `ManagedApplicationFixture<TEntryPoint>` when an xUnit class fixture should exercise the application's real entry point and let that entry point start the host. This is an opt-in path for the current minor release. Fixture setup remains lazy; accessing the test host starts the deferred host and surfaces startup failures at the point the test consumes it. The legacy blocking path is retained for compatibility until it can be removed or changed in the next major release.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace InventoryWorker.Tests;

public sealed class InventoryWorkerTest : IClassFixture<ManagedApplicationFixture<WorkerProgram>>
{
    private readonly ManagedApplicationFixture<WorkerProgram> _fixture;

    public InventoryWorkerTest(ManagedApplicationFixture<WorkerProgram> fixture)
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
