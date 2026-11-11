---
uid: Codebelt.Extensions.Xunit.WildcardOptions
example:
- *content
---

Configure wildcard matching to throw when a value does not match the expected pattern.

```csharp
using Codebelt.Extensions.Xunit;
using Xunit;

namespace Example.Tests;

public class WildcardExampleTest : Test
{
    public WildcardExampleTest(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void ShouldMatchConfiguredPattern()
    {
        var options = new WildcardOptions { ThrowOnNoMatch = true };
        Assert.True(Match("cache*", "cached", setup => setup.ThrowOnNoMatch = options.ThrowOnNoMatch));
    }
}
```

[!INCLUDE [availability-default](../../includes/availability-default.md)]
