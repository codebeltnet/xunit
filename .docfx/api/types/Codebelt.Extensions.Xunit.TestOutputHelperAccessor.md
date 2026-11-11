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

[!INCLUDE [availability-default](../../includes/availability-default.md)]
