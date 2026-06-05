---
uid: Codebelt.Extensions.Xunit.Hosting
summary: *content
---
The `Codebelt.Extensions.Xunit.Hosting` namespace contains types that provides a uniform way of doing unit testing that is used in conjunction with Microsoft Dependency Injection. The namespace relates to the `Xunit.Abstractions` namespace.

[!INCLUDE [availability-default](../../includes/availability-default.md)]

Complements: [xUnit: Shared Context between Tests](https://xunit.net/docs/shared-context) 🔗

### Fixture Naming Convention

Host fixtures follow a lifecycle naming convention:

|Prefix|Convention|
|---|---|
|`Managed`|The fixture owns host creation, configuration, startup and disposal using the default host runner.|
|`SelfManaged`|The fixture owns host creation and configuration, but leaves host startup to the test.|
|`BlockingManaged`|The fixture owns the host lifecycle and starts the host synchronously before returning control to the test.|

Application-entry-point fixtures use the `BlockingManaged` prefix by default. Existing application entry points are discovered and built from their `Program` assembly, so tests should receive a ready host after fixture initialization. Use `BlockingManagedApplicationFixture<TEntryPoint>` when testing console, worker or generic host applications from an existing entry point.

### Extension Methods

|Type|Ext|Methods|
|--:|:-:|---|
|ILogger{T}|⬇️|`GetTestStore`|
|IServiceCollection|⬇️|`AddXunitTestOutputHelperAccessor`|
|IServiceProvider|⬇️|`GetRequiredScopedService`|
