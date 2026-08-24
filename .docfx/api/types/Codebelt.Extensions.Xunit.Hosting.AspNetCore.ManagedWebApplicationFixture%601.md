---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.ManagedWebApplicationFixture`1
example:
- *content
---

Use `ManagedWebApplicationFixture<TEntryPoint>` when an xUnit class fixture should exercise an ASP.NET Core application's real entry point and let that entry point start the in-memory server. Derive the test from `WebApplicationTest<TEntryPoint,T>` and pass the fixture to its base constructor so the base class initializes the fixture through `ConfigureHost` before the test reads `Server`. Fixture setup remains lazy; consuming the test host starts the deferred host, after which the test can create a client from the exposed `TestServer` and verify the application's endpoint behavior. `WebApplicationTestFactory` uses this fixture by default.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CatalogApi.Tests;

public sealed class CatalogApiTest : WebApplicationTest<CatalogProgram, ManagedWebApplicationFixture<CatalogProgram>>
{
    public CatalogApiTest(ManagedWebApplicationFixture<CatalogProgram> fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
    }

    [Fact]
    public async Task HealthEndpoint_ReturnsApplicationState()
    {
        using var client = Server.CreateClient();

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
