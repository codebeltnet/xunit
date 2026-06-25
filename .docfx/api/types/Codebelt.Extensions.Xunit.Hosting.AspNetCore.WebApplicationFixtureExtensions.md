---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebApplicationFixtureExtensions
example:
- *content
---

The following example validates that an `IWebApplicationFixture<TEntryPoint>` has a valid initialized state by calling `HasValidState<TEntryPoint>`. The method checks that the host, configure callback, and configure-web-host callback are all non-null.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;

namespace WebFixtureTests;

public class WebAppStateGuard
{
    public bool EnsureFixtureIsReady<TEntryPoint>(IWebApplicationFixture<TEntryPoint> fixture) where TEntryPoint : class
    {
        return fixture.HasValidState<TEntryPoint>();
    }
}
```
