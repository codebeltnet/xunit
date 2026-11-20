---
uid: Codebelt.Extensions.Xunit.TestOutputHelperExtensions
example:
- *content
---

Write several values to xUnit output as separate lines when diagnosing a failing test.

```csharp
using Codebelt.Extensions.Xunit;
using Xunit;

namespace Example.Tests;

public class OutputLinesTest : Test
{
    public OutputLinesTest(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void ShouldWriteValuesOnSeparateLines()
    {
        TestOutput.WriteLines("started", 2, "finished");
        Assert.True(HasTestOutput);
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="45fcccef5fc861315b50c55e"><a href="~/run-actions/45fcccef5fc861315b50c55e/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/45fcccef5fc861315b50c55e/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->

[!INCLUDE [availability-default](../../includes/availability-default.md)]

---
uid: Codebelt.Extensions.Xunit.TestOutputHelperExtensions.WriteLines(Xunit.ITestOutputHelper,System.Object[])
example:
- *content
---

Pass mixed values to the object overload to write one line per value.

```csharp
using Codebelt.Extensions.Xunit;
using Xunit;

namespace Example.Tests;

public class MixedOutputTest : Test
{
    public MixedOutputTest(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void ShouldWriteMixedValues()
    {
        TestOutput.WriteLines((object)"cache", 3);
        Assert.True(HasTestOutput);
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="578d467fd203f66cc41b0364"><a href="~/run-actions/578d467fd203f66cc41b0364/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/578d467fd203f66cc41b0364/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->

---
uid: Codebelt.Extensions.Xunit.TestOutputHelperExtensions.WriteLines``1(Xunit.ITestOutputHelper,``0[])
example:
- *content
---

Pass an array to the generic overload when all values have the same type.

```csharp
using Codebelt.Extensions.Xunit;
using Xunit;

namespace Example.Tests;

public class ArrayOutputTest : Test
{
    public ArrayOutputTest(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void ShouldWriteArrayItems()
    {
        TestOutput.WriteLines(new int[] { 1, 2, 3 });
        Assert.True(HasTestOutput);
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="10457ac34cce1cbadb26aee1"><a href="~/run-actions/10457ac34cce1cbadb26aee1/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/10457ac34cce1cbadb26aee1/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->

---
uid: Codebelt.Extensions.Xunit.TestOutputHelperExtensions.WriteLines``1(Xunit.ITestOutputHelper,System.Collections.Generic.IEnumerable{``0})
example:
- *content
---

Pass an enumerable to write values produced by a query without converting it to an array.

```csharp
using System.Collections.Generic;
using Codebelt.Extensions.Xunit;
using Xunit;

namespace Example.Tests;

public class EnumerableOutputTest : Test
{
    public EnumerableOutputTest(ITestOutputHelper output) : base(output) { }

    [Fact]
    public void ShouldWriteSequenceItems()
    {
        IEnumerable<int> values = new List<int> { 1, 2, 3 };
        TestOutput.WriteLines(values);
        Assert.True(HasTestOutput);
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="0308118a47b5c02daa7b32be"><a href="~/run-actions/0308118a47b5c02daa7b32be/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/0308118a47b5c02daa7b32be/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
