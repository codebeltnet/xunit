---
uid: Codebelt.Extensions.Xunit.Hosting.MinimalHostFixtureExtensions
example:
- *content
---

The following example validates that an `IMinimalHostFixture` has been fully initialized by calling `HasValidState` on the fixture. The method checks that the host, configure-host callback, and configure callback are all non-null.

```csharp
using Codebelt.Extensions.Xunit.Hosting;

namespace HostFixtureTests;

public class HostStateGuard
{
    public bool EnsureFixtureIsReady(IMinimalHostFixture fixture)
    {
        return fixture.HasValidState();
    }
}
```
