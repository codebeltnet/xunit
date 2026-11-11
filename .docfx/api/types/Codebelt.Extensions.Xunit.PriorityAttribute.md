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

[!INCLUDE [availability-default](../../includes/availability-default.md)]
