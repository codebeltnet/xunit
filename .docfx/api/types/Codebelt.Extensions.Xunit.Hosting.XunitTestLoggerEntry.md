---
uid: Codebelt.Extensions.Xunit.Hosting.XunitTestLoggerEntry
example:
- *content
---

The following example creates an `XunitTestLoggerEntry` record and formats its message as a string. Each entry captures the `LogLevel`, `EventId`, and message text of a single log statement written during a test, and the record's `ToString()` method returns the message for easy inspection in assertions.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;

namespace HostFixtureTests;

public class LogEntryExample
{
    public string FormatEntry()
    {
        var entry = new XunitTestLoggerEntry(LogLevel.Warning, new EventId(1001, "MyEvent"), "Something unexpected happened.");
        return entry.ToString();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        Assert.Equal("Something unexpected happened.", new LogEntryExample().FormatEntry());
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="9a558a9a581c6b11ccb28057"><a href="~/run-actions/9a558a9a581c6b11ccb28057/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/9a558a9a581c6b11ccb28057/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
