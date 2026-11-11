---
uid: Codebelt.Extensions.Xunit.PriorityTestMethodOrderer
example:
- *content
---

Apply the orderer to a test class to run its methods by `[Priority(n)]`. Negative values run before zero; methods without the attribute run last.

```csharp
using System;
using Codebelt.Extensions.Xunit;
using Xunit;
using Xunit.Sdk;

namespace Example.Tests;

[TestMethodOrderer(typeof(PriorityTestMethodOrderer))]
public class CacheLifecycleTest : Test
{
    private static bool _initialized;

    public CacheLifecycleTest(ITestOutputHelper output) : base(output) { }

    [Fact, Priority(-1)]
    public void ShouldInitializeCache()
    {
        var orderer = new PriorityTestMethodOrderer();
        Assert.Empty(orderer.OrderTestMethods<ITestMethod>(Array.Empty<ITestMethod>()));
        _initialized = true;
    }

    [Fact, Priority(0)]
    public void ShouldUseInitializedCache()
    {
        Assert.True(_initialized);
    }
}
```

[!INCLUDE [availability-default](../../includes/availability-default.md)]
