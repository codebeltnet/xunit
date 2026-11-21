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

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="038a1c806225b412a6f986c1"><a href="~/run-actions/038a1c806225b412a6f986c1/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/038a1c806225b412a6f986c1/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
