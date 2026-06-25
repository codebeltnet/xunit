---
uid: Codebelt.Extensions.Xunit.Hosting.ServiceCollectionExtensions
example:
- *content
---

The following example registers xUnit test logging and output helper services on an `IServiceCollection` so that `ILogger<T>` output is captured during a test run. Call `AddXunitTestLogging` with a minimum `LogLevel`, then register the `ITestOutputHelperAccessor` using `AddXunitTestLoggingOutputHelperAccessor` or a custom implementation with `AddXunitTestLoggingOutputHelperAccessor<T>`.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HostFixtureTests;

public class ExampleFixture
{
    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services.AddXunitTestLogging(LogLevel.Information);
        services.AddXunitTestLoggingOutputHelperAccessor();
        return services;
    }
}
```
