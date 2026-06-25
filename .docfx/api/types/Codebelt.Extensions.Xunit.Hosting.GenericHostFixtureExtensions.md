---
uid: Codebelt.Extensions.Xunit.Hosting.GenericHostFixtureExtensions
example:
- *content
---

The following example validates that an `IGenericHostFixture` has been fully configured by calling `HasValidState` on the fixture. The method returns `true` only when both `ConfigureServicesCallback` and `ConfigureHostCallback` are non-null.

```csharp
using Codebelt.Extensions.Xunit.Hosting;

namespace HostFixtureTests;

public class HostStateGuard
{
    public bool EnsureFixtureIsReady(IGenericHostFixture fixture)
    {
        return fixture.HasValidState();
    }
}
```
