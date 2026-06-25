---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.ServiceCollectionExtensions
example:
- *content
---

The following example registers a fake `IHttpContextAccessor` on an `IServiceCollection` so that unit tests can simulate HTTP context without hosting a real server. Call `AddFakeHttpContextAccessor` with the desired service lifetime to control how the accessor is reused across requests.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebFixtureTests;

public class ExampleFixture
{
    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        return services.AddFakeHttpContextAccessor(ServiceLifetime.Scoped);
    }
}
```
