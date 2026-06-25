---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features
summary: *content
---

Simulate the ASP.NET Core HTTP request and response feature surfaces in unit tests without running a real server. The `Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features` namespace provides `FakeHttpRequestFeature` and `FakeHttpResponseFeature`, which implement `IHttpRequestFeature` and `IHttpResponseFeature` with settable properties backed by default values. Use these fakes when your test code reads request headers, the request body, the response status code, or response headers from the `IFeatureCollection` on `HttpContext.Features`.

Start with `FakeHttpRequestFeature` when you need to control the HTTP method, path, query string, or request body in isolation. Use `FakeHttpResponseFeature` when the code under test writes status codes, response headers, or the response body.

[!INCLUDE [availability-modern](../../includes/availability-modern.md)]

Complements: [Microsoft.AspNetCore.Http.Features namespace](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.features) 🔗