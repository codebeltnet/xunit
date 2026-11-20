---
uid: Codebelt.Extensions.Xunit.Hosting.LoggerExtensions
example:
- *content
---

The following example retrieves the captured log entries from an `ILogger<T>` after xUnit test logging has been registered. Use `GetTestStore()` on an untyped `ILogger` to search by category name, or `GetTestStore<T>()` to retrieve entries for a specific typed logger category.

```csharp
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace HostFixtureTests;

public class LogInspector
{
    public ITestStore<XunitTestLoggerEntry> GetEntries(ILogger logger)
    {
        return logger.GetTestStore();
    }

    public ITestStore<XunitTestLoggerEntry> GetEntries<T>(ILogger<T> logger)
    {
        return logger.GetTestStore<T>();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        var services = new ServiceCollection();
        services.AddXunitTestLogging();
        using var provider = services.BuildServiceProvider();
        var logger = provider.GetRequiredService<ILogger<LogInspector>>();
        logger.LogWarning("queue ready");
        var inspector = new LogInspector();
        Assert.Contains(inspector.GetEntries((ILogger)logger).Query(), entry => entry.Severity == LogLevel.Warning && entry.Message == "Warning: queue ready");
        Assert.Contains(inspector.GetEntries(logger).Query(), entry => entry.Severity == LogLevel.Warning && entry.Message == "Warning: queue ready");
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="513a785b0e0abbe2284270b9"><a href="~/run-actions/513a785b0e0abbe2284270b9/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/513a785b0e0abbe2284270b9/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
