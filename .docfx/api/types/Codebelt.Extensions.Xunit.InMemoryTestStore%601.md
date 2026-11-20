---
uid: Codebelt.Extensions.Xunit.InMemoryTestStore`1
example:
- *content
---

Use the store to retain test data and query the matching items without a database.

```csharp
using System.Linq;
using Codebelt.Extensions.Xunit;
using Xunit;

namespace Example.Tests;

public class StoreExample
{
    public bool HasMatchingItem()
    {
        var store = new InMemoryTestStore<string>();
        store.Add("cached");
        store.Add("other");
        return store.Count == 2 && store.Query(value => value == "cached").Single() == "cached";
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        Assert.True(new StoreExample().HasMatchingItem());
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="a31971b360a8c557b87f6684"><a href="~/run-actions/a31971b360a8c557b87f6684/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/a31971b360a8c557b87f6684/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->

[!INCLUDE [availability-default](../../includes/availability-default.md)]
