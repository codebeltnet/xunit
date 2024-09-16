## About

An open-source project (MIT license) that targets and complements the xUnit.net test platform. It provides a uniform and convenient way of doing unit test for all project types in .NET.

It is, by heart, free, flexible and built to extend and boost your agile codebelt.

## **Codebelt.Extensions.Xunit.Hosting.AspNetCore** for .NET

The `Codebelt.Extensions.Xunit.Hosting.AspNetCore` namespace contains types that provides a uniform way of doing unit testing that depends on ASP.NET Core and used in conjunction with Microsoft Dependency Injection. The namespace relates to the `Microsoft.AspNetCore.TestHost` namespace.

More documentation available at our documentation site:

- [Codebelt.Extensions.Xunit.Hosting.AspNetCore](https://xunit.codebelt.net/api/Codebelt.Extensions.Xunit.Hosting.AspNetCore.html) 🔗
- [Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http](https://xunit.codebelt.net/api/Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.html) 🔗
- [Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features](https://xunit.codebelt.net/api/Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features.html) 🔗

## Related Packages

* [Codebelt.Extensions.Xunit](https://www.nuget.org/packages/Codebelt.Extensions.Xunit/) 📦
* [Codebelt.Extensions.Xunit.App](https://www.nuget.org/packages/Codebelt.Extensions.Xunit.App/) 🏭
* [Codebelt.Extensions.Xunit.Hosting](https://www.nuget.org/packages/Codebelt.Extensions.Xunit.Hosting/) 📦
* [Codebelt.Extensions.Xunit.Hosting.AspNetCore](https://www.nuget.org/packages/Codebelt.Extensions.Xunit.Hosting.AspNetCore/) 📦

### CSharp Example

Source: [ServerTimingMiddlewareTest.cs](https://github.com/gimlichael/Cuemon/blob/main/test/Cuemon.AspNetCore.Tests/Diagnostics/ServerTimingMiddlewareTest.cs)

```csharp
[Fact]
public async Task InvokeAsync_ShouldMimicSimpleAspNetProject()
{
    using var response = await WebHostTestFactory.RunAsync(
        services =>
        {
            services.AddServerTiming(o => o.SuppressHeaderPredicate = _ => false);
        }
        , app =>
        {
            app.UseServerTiming();
            app.Use(async (context, next) =>
            {
                var sw = Stopwatch.StartNew();
                context.Response.OnStarting(() =>
                {
                    sw.Stop();
                    context.RequestServices.GetRequiredService<IServerTiming>().AddServerTiming("use-middleware", sw.Elapsed);
                    return Task.CompletedTask;
                });
                await next(context).ConfigureAwait(false);
            });
            app.Run(context =>
            {
                Thread.Sleep(400);
                return context.Response.WriteAsync("Hello World!");
            });
        }).ConfigureAwait(false);

    Assert.StartsWith("use-middleware;dur=", response.Headers.Single(kvp => kvp.Key == ServerTiming.HeaderName).Value.FirstOrDefault());
}
```
