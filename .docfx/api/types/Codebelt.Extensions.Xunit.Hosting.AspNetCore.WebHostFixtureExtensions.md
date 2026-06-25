---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebHostFixtureExtensions
example:
- *content
---

The following example validates that an `IWebHostFixture` has a valid initialized state by calling `HasValidState`. The method checks that the underlying generic host fixture is valid and that the application configure callback is set.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;

namespace WebFixtureTests;

public class WebHostStateGuard
{
    public bool EnsureFixtureIsReady(IWebHostFixture fixture)
    {
        return fixture.HasValidState();
    }
}
```
