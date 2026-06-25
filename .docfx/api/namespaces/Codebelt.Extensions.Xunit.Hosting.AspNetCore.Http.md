---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http
summary: *content
---

Isolate unit tests from the real `IHttpContextAccessor` and `HttpContext` pipeline. The `Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http` namespace provides `FakeHttpContextAccessor`, a test double that implements `IHttpContextAccessor` with settable `HttpContext` and `HttpContextFactory` properties, letting you simulate request context without hosting a full server. Use this type when your tested code depends on `IHttpContextAccessor` and you need deterministic, fast test setup.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

Complements: [Microsoft.AspNetCore.Http namespace](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http) 🔗