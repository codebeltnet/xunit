---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore
summary: *content
---

Exercise an ASP.NET Core application's real entry point, dependency-injection graph, middleware pipeline, and endpoints through an in-memory `TestServer`. The `Codebelt.Extensions.Xunit.Hosting.AspNetCore` namespace can bootstrap modern minimal hosting and conventional `Startup` applications, apply test-only web-host configuration, and return either an owned test context or a reusable xUnit fixture.

For a focused endpoint or service-override test, start with `WebApplicationTestFactory.Create<TEntryPoint>` or its one-request `RunAsync<TEntryPoint>` convenience. Use `WebApplicationTest<TEntryPoint, TFixture>` with `ManagedWebApplicationFixture<TEntryPoint>` when several tests should share the bootstrapped application. Reach for `WebHostTestFactory` or `MinimalWebHostTestFactory` when the test defines its own pipeline instead of loading an existing application.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

Complements: [ASP.NET Core integration tests](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-10.0) · [WebApplicationFactory<TEntryPoint>](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1?view=aspnetcore-10.0) · [Microsoft.AspNetCore.TestHost namespace](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.testhost) 🔗

### Choose an ASP.NET Core Testing Path

|When you need to|Start with|Why|
|---|---|---|
|Bootstrap an existing ASP.NET Core application for one focused test|`WebApplicationTestFactory.Create<TEntryPoint>`|Returns an owned `IHostTest` whose host exposes the application's `TestServer`, services, configuration, and environment.|
|Send one request to an existing application|`WebApplicationTestFactory.RunAsync<TEntryPoint>`|Combines application startup, `HttpClient` creation, request execution, and cleanup in one call.|
|Share an existing application across an xUnit test class|`WebApplicationTest<TEntryPoint, TFixture>` with `ManagedWebApplicationFixture<TEntryPoint>`|Uses entrypoint-owned startup while the fixture exposes `TestServer`.|
|Define services and middleware entirely inside the test|`WebHostTestFactory` or `MinimalWebHostTestFactory`|Builds a purpose-specific in-memory pipeline without loading an application project.|
|Attach observers or change state before startup|A `SelfManaged` web fixture|Builds the host and pipeline but leaves startup to the test.|

### Compared with WebApplicationFactory

`WebApplicationTestFactory` is an alternative integration-test entry point, not a drop-in replacement for `Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<TEntryPoint>`.

|Concern|`WebApplicationTestFactory`|Microsoft `WebApplicationFactory<TEntryPoint>`|
|---|---|---|
|Acquisition|Static `Create<TEntryPoint>` or `RunAsync<TEntryPoint>` returns a Codebelt test context.|Instantiate or inject a factory, then call `CreateClient`.|
|Customization|Pass an `IWebHostBuilder` callback at the call site.|Subclass and override `ConfigureWebHost`, or compose with `WithWebHostBuilder`.|
|Sharing|Use Codebelt's `WebApplicationTest` and fixture types when tests should share a context.|Commonly shared directly through xUnit `IClassFixture<WebApplicationFactory<TEntryPoint>>`.|
|Lifecycle|The caller disposes the returned context, or delegates ownership to a Codebelt fixture.|The factory owns its `TestServer` and clients and is disposed by the caller or xUnit fixture lifecycle.|
|Scope|Matches Codebelt's equivalent entry-point pattern for console, worker, and Generic Host applications.|Purpose-built for ASP.NET Core applications and includes MVC-testing conventions such as client options and content-root discovery.|

### Fixture Naming Convention

ASP.NET Core host fixtures follow the same lifecycle naming convention as the hosting package:

|Prefix|Convention|
|---|---|
|`Managed`|The fixture owns host creation, configuration and disposal while the application entry point owns startup; test-host consumption starts the deferred host when needed.|
|`SelfManaged`|The fixture owns host creation and configuration, but leaves host startup to the test.|

`WebApplicationTestFactory` uses `ManagedWebApplicationFixture<TEntryPoint>` by default, so the real application entry point owns startup and fixture setup remains lazy. Use `ManagedWebApplicationFixture<TEntryPoint>` explicitly with `WebApplicationTest<TEntryPoint, TFixture>` when tests share the application context; use a `SelfManaged` fixture when the test must control startup itself.

`BlockingManagedWebHostFixture` remains the opt-in blocking variant for the lower-level web host fixture family. It is separate from the application-entry-point fixture, which uses `ManagedWebApplicationFixture<TEntryPoint>` for entrypoint-owned startup.

### Extension Members

|Type|Ext|Methods|
|--:|:-:|---|
|HttpClient|⬇️|`ToHttpResponseMessageAsync`|
|IHostApplicationBuilder|⬇️|`ToHostBuilder`|
|IServiceCollection|⬇️|`AddFakeHttpContextAccessor`|
|IWebApplicationFixture&lt;TEntryPoint&gt;|⬇️|`HasValidState<TEntryPoint>`|
|IWebHostFixture|⬇️|`HasValidState`|
|IWebMinimalHostFixture|⬇️|`HasValidState`|
