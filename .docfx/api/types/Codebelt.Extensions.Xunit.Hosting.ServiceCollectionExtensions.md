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
using Xunit;

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

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        using var provider = new ExampleFixture().ConfigureServices(new ServiceCollection()).BuildServiceProvider();
        Assert.IsType<Codebelt.Extensions.Xunit.TestOutputHelperAccessor>(provider.GetRequiredService<Codebelt.Extensions.Xunit.ITestOutputHelperAccessor>());
        var logger = provider.GetRequiredService<ILogger<RunExample>>();
        logger.LogInformation("storage ready");
        Assert.Contains(logger.GetTestStore().Query(), entry => entry.Severity == LogLevel.Information && entry.Message == "Information: storage ready");
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="18b0746bf3da1d84f81b5274"><a href="~/run-actions/18b0746bf3da1d84f81b5274/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/18b0746bf3da1d84f81b5274/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
