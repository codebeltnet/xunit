---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.HostBuilderApplicationExtensions
example:
- *content
---

The following example converts an `IHostApplicationBuilder` to an `IHostBuilder` by calling `ToHostBuilder`. This is useful when working with ASP.NET Core minimal API hosts where the `IHostApplicationBuilder` provides access to the underlying `IHostBuilder` for advanced configuration.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.Extensions.Hosting;

namespace WebFixtureTests;

public class BuilderConverter
{
    public IHostBuilder Convert(IHostApplicationBuilder builder)
    {
        return builder.ToHostBuilder();
    }
}
```
