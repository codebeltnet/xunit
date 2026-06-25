---
uid: Codebelt.Extensions.Xunit.Hosting.ApplicationFixtureExtensions
example:
- *content
---

The following example validates that an `IApplicationFixture<TEntryPoint>` has been fully initialized with host, configure callback, and configure-host callback by calling `HasValidState<TEntryPoint>` on the fixture. This is useful in assertion helpers to confirm the fixture was set up correctly before running the test scenario.

```csharp
using Codebelt.Extensions.Xunit.Hosting;

namespace HostFixtureTests;

public class HostStateGuard
{
    public bool EnsureFixtureIsReady<TEntryPoint>(IApplicationFixture<TEntryPoint> fixture) where TEntryPoint : class
    {
        return fixture.HasValidState<TEntryPoint>();
    }
}
```
