---
uid: Codebelt.Extensions.Xunit.TestOutputHelperAccessor
example:
- *content
---

Create an accessor from a test's output helper and retrieve that helper when another component needs to write test output.

```csharp
using Codebelt.Extensions.Xunit;
using Xunit;

namespace Example.Tests;

public class OutputAccessorTest : Test
{
    public OutputAccessorTest(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void ShouldExposeCurrentOutput()
    {
        var accessor = new TestOutputHelperAccessor(TestOutput);
        Assert.Same(TestOutput, accessor.TestOutput);
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="545543e49c6bf054a9e06186"><a href="~/run-actions/545543e49c6bf054a9e06186/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/545543e49c6bf054a9e06186/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->

[!INCLUDE [availability-default](../../includes/availability-default.md)]
