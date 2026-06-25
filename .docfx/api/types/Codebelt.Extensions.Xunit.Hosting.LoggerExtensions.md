---
uid: Codebelt.Extensions.Xunit.Hosting.LoggerExtensions
example:
- *content
---

The following example retrieves the captured log entries from an `ILogger<T>` after xUnit test logging has been registered. Use `GetTestStore()` on an untyped `ILogger` to search by category name, or `GetTestStore<T>()` to retrieve entries for a specific typed logger category.

```csharp
using Codebelt.Extensions.Xunit;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.Logging;

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
```
