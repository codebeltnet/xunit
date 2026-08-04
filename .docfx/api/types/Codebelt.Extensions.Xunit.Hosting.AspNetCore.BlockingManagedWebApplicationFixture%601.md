---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.BlockingManagedWebApplicationFixture`1
example:
- *content
---

The test project references a minimal ASP.NET Core application and shares its in-memory server through xUnit's class-fixture lifetime. `BlockingManagedWebApplicationFixture<TEntryPoint>` is an obsolete compatibility fixture that preserves the legacy blocking startup path for the current minor release; new tests should use `ManagedWebApplicationFixture<TEntryPoint>` so the real application entry point owns startup. This compatibility type should be removed or changed in the next major release.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CatalogApi.Tests;

public sealed class CatalogApiTest : IClassFixture<BlockingManagedWebApplicationFixture<CatalogProgram>>
{
    private readonly BlockingManagedWebApplicationFixture<CatalogProgram> _fixture;

    public CatalogApiTest(BlockingManagedWebApplicationFixture<CatalogProgram> fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task HealthEndpoint_ReturnsApplicationState()
    {
        using var client = _fixture.Server.CreateClient();

        var body = await client.GetStringAsync("/health").ConfigureAwait(false);

        Assert.Equal("ready", body);
    }
}

public sealed record CatalogStatus(string Value);

public sealed class CatalogProgram
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSingleton(new CatalogStatus("ready"));

        var app = builder.Build();
        app.MapGet("/health", (CatalogStatus status) => status.Value);
        app.Run();
    }
}
```
