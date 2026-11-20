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

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="782f24d34260070258f6d164"><a href="~/run-actions/782f24d34260070258f6d164/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/782f24d34260070258f6d164/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->

[!INCLUDE [availability-default](../../includes/availability-default.md)]
