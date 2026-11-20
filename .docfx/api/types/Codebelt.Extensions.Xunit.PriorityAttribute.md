---
uid: Codebelt.Extensions.Xunit.PriorityAttribute
example:
- *content
---

The following test checks an initialization step before using the initialized value. The attribute supplies the numeric value consumed by `PriorityTestMethodOrderer`.

```csharp
using Codebelt.Extensions.Xunit;
using Xunit;

namespace Example.Tests;

[TestMethodOrderer(typeof(PriorityTestMethodOrderer))]
public class InitializationTest : Test
{
    private static string _value;

    public InitializationTest(ITestOutputHelper output) : base(output) { }

    [Fact, Priority(0)]
    public void ShouldInitializeFirst()
    {
        var priority = new PriorityAttribute(0);
        Assert.Equal(0, priority.Priority);
        _value = "ready";
    }

    [Fact, Priority(1)]
    public void ShouldReadInitializedValue()
    {
        Assert.Equal("ready", _value);
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="812efaa205554c7bdcd80831"><a href="~/run-actions/812efaa205554c7bdcd80831/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/812efaa205554c7bdcd80831/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->

[!INCLUDE [availability-default](../../includes/availability-default.md)]
