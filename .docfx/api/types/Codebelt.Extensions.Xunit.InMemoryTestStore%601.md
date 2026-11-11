---
uid: Codebelt.Extensions.Xunit.InMemoryTestStore`1
example:
- *content
---

Use the store to retain test data and query the matching items without a database.

```csharp
using System.Linq;
using Codebelt.Extensions.Xunit;

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
```

[!INCLUDE [availability-default](../../includes/availability-default.md)]
