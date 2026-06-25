---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebApplicationTestFactory
example:
- *content
---

The test project references a minimal ASP.NET Core application with a `/health` endpoint. `WebApplicationTestFactory` runs that real entry point on `TestServer`, applies a test-only service override through `IWebHostBuilder`, and gives the test an owned host from which it creates an HTTP client.

```csharp
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CatalogApi.Tests;

public sealed class WebApplicationTestFactoryExample
{
    [Fact]
    public async Task Create_UsesTheApplicationPipelineAndTestOverrides()
    {
        using var application = WebApplicationTestFactory.Create<CatalogProgram>(builder =>
        {
            builder.ConfigureServices(services =>
                services.AddSingleton(new CatalogStatus("test-ready")));
        });
        using var client = application.Host.GetTestClient();

        var body = await client.GetStringAsync("/health").ConfigureAwait(false);

        Assert.Equal("test-ready", body);
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
