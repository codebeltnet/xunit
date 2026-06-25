---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.BlockingManagedWebHostFixture
example:
- *content
---

The following example uses `BlockingManagedWebHostFixture` as a fixture for xUnit tests that need a synchronously started web host. The fixture manages the full web host lifecycle and blocks until the host is started before returning control to the test.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace WebFixtureTests;

public class WebHostIntegrationTest : IClassFixture<BlockingManagedWebHostFixture>
{
    private readonly BlockingManagedWebHostFixture _fixture;

    public WebHostIntegrationTest(BlockingManagedWebHostFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void HostShouldBeStarted()
    {
        Assert.True(_fixture.HasValidState());
    }
}
```
