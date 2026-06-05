---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore
summary: *content
---
The `Codebelt.Extensions.Xunit.Hosting.AspNetCore` namespace contains types that provides a uniform way of doing unit testing that depends on ASP.NET Core and used in conjunction with Microsoft Dependency Injection. The namespace relates to the `Microsoft.AspNetCore.TestHost` namespace.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

Complements: [Microsoft.AspNetCore.TestHost namespace](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.testhost) 🔗

### Fixture Naming Convention

ASP.NET Core host fixtures follow the same lifecycle naming convention as the hosting package:

|Prefix|Convention|
|---|---|
|`Managed`|The fixture owns host creation, configuration, startup and disposal using the default host runner.|
|`SelfManaged`|The fixture owns host creation and configuration, but leaves host startup to the test.|
|`BlockingManaged`|The fixture owns the host lifecycle and starts the host synchronously before returning control to the test.|

Application-entry-point fixtures use the `BlockingManaged` prefix by default. ASP.NET Core application tests expose a `TestServer`, and callers should receive a started server after fixture initialization. Use `BlockingManagedWebApplicationFixture<TEntryPoint>` when testing an existing ASP.NET Core application entry point with `TestServer`.

`BlockingManagedWebHostFixture` remains the opt-in blocking variant for the lower-level web host fixture family. The application-entry-point fixture is named `BlockingManagedWebApplicationFixture<TEntryPoint>` directly because this API is blocking by convention from its first release.

### Extension Methods

|Type|Ext|Methods|
|--:|:-:|---|
|HttpClient|⬇️|`ToHttpResponseMessageAsync`|
|IServiceCollection|⬇️|`AddFakeHttpContextAccessor`|
