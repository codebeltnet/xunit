---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebMinimalHostFixtureExtensions
example:
- *content
---

The following example validates that an `IWebMinimalHostFixture` has a valid initialized state by calling `HasValidState`. The method checks the minimal host fixture state and additionally verifies that the application callback and application pipeline are configured.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;

namespace WebFixtureTests;

public class WebMinimalHostStateGuard
{
    public bool EnsureFixtureIsReady(IWebMinimalHostFixture fixture)
    {
        return fixture.HasValidState();
    }
}
```
