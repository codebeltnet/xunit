---
uid: Codebelt.Extensions.Xunit.Hosting
summary: *content
---

Exercise a real .NET application entry point, hosted service, or dependency-injection graph without rebuilding its host setup inside the test project. The `Codebelt.Extensions.Xunit.Hosting` namespace applies the same test model to console apps, workers, and Generic Host applications: bootstrap the application's `Program` assembly, customize the host for the scenario, inspect configuration and services, and dispose the test host through one `IHostTest` abstraction.

For a focused test, start with `ApplicationTestFactory.Create<TEntryPoint>`. It is the non-web counterpart to the entry-point pattern commonly associated with ASP.NET Core's `WebApplicationFactory<TEntryPoint>`: the generic argument identifies the application assembly, while the returned test context exposes the resulting host, configuration, and environment. Use the fixture/base-class form when several xUnit tests should share that application context.

[!INCLUDE [availability-default](../../includes/availability-default.md)]

Complements: [xUnit: Shared Context between Tests](https://xunit.net/docs/shared-context) 🔗

### Choose a Hosting Path

|When you need to|Start with|Why|
|---|---|---|
|Bootstrap an existing console, worker, or Generic Host application for one test|`ApplicationTestFactory.Create<TEntryPoint>`|Runs the application's entry-point setup and returns an owned `IHostTest` context that the caller disposes.|
|Share an existing application across an xUnit test class|`ApplicationTest<TEntryPoint, TFixture>` with `BlockingManagedApplicationFixture<TEntryPoint>`|Moves application startup and disposal into xUnit's fixture lifecycle while retaining configuration and service access.|
|Build a conventional Generic Host entirely inside the test|`HostTestFactory`|Configures `IServiceCollection` and `IHostBuilder` directly without requiring an application entry point.|
|Build with the modern `IHostApplicationBuilder` model|`MinimalHostTestFactory`|Keeps minimal-host tests focused on services and application-builder configuration.|
|Configure the host now but decide when it starts|A `SelfManaged` fixture|Leaves startup under test control so observers and pre-start assertions can be attached first.|

### Fixture Naming Convention

Host fixtures follow a lifecycle naming convention:

|Prefix|Convention|
|---|---|
|`Managed`|The fixture owns host creation, configuration, startup and disposal using the default host runner.|
|`SelfManaged`|The fixture owns host creation and configuration, but leaves host startup to the test.|
|`BlockingManaged`|The fixture owns the host lifecycle and starts the host synchronously before returning control to the test.|

Application-entry-point fixtures use the `BlockingManaged` prefix by default. Existing application entry points are discovered and built from their `Program` assembly, so tests receive a ready host after fixture initialization. Use `BlockingManagedApplicationFixture<TEntryPoint>` when testing a console, worker, or Generic Host application from an existing entry point.

### Extension Members

|Type|Ext|Methods|
|--:|:-:|---|
|IApplicationFixture&lt;TEntryPoint&gt;|⬇️|`HasValidState<TEntryPoint>`|
|IGenericHostFixture|⬇️|`HasValidState`|
|ILogger|⬇️|`GetTestStore`|
|ILogger&lt;T&gt;|⬇️|`GetTestStore<T>`|
|IMinimalHostFixture|⬇️|`HasValidState`|
|IServiceCollection|⬇️|`AddXunitTestLogging` · `AddXunitTestLoggingOutputHelperAccessor` · `AddXunitTestLoggingOutputHelperAccessor<T>`|
|IServiceProvider|⬇️|`GetRequiredScopedService<T>`|
