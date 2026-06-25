---
uid: Codebelt.Extensions.Xunit.Hosting.XunitTestLoggerEntry
example:
- *content
---

The following example creates an `XunitTestLoggerEntry` record and formats its message as a string. Each entry captures the `LogLevel`, `EventId`, and message text of a single log statement written during a test, and the record's `ToString()` method returns the message for easy inspection in assertions.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.Logging;

namespace HostFixtureTests;

public class LogEntryExample
{
    public string FormatEntry()
    {
        var entry = new XunitTestLoggerEntry(LogLevel.Warning, new EventId(1001, "MyEvent"), "Something unexpected happened.");
        return entry.ToString();
    }
}
```
