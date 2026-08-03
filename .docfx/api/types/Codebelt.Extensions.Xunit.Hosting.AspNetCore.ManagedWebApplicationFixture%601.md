---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.ManagedWebApplicationFixture`1
example:
- *content
---

Use `ManagedWebApplicationFixture<TEntryPoint>` when an xUnit class fixture should exercise an ASP.NET Core application's real entry point and let that entry point start the in-memory server. This is an opt-in path for the current minor release. Fixture setup remains lazy; consuming the test host starts the deferred host, after which the test can create a client from the exposed `TestServer` and verify the application's endpoint behavior. The legacy blocking path is retained for compatibility until it can be removed or changed in the next major release.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CatalogApi.Tests;

public sealed class CatalogApiTest : IClassFixture<ManagedWebApplicationFixture<CatalogProgram>>
{
    private readonly ManagedWebApplicationFixture<CatalogProgram> _fixture;

    public CatalogApiTest(ManagedWebApplicationFixture<CatalogProgram> fixture)
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
