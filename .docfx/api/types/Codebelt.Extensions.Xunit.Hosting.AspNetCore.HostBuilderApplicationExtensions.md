---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.HostBuilderApplicationExtensions
example:
- *content
---

The following example converts an `IHostApplicationBuilder` to an `IHostBuilder` by calling `ToHostBuilder`. This is useful when working with ASP.NET Core minimal API hosts where the `IHostApplicationBuilder` provides access to the underlying `IHostBuilder` for advanced configuration.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace WebFixtureTests;

public class BuilderConverter
{
    public IHostBuilder Convert(IHostApplicationBuilder builder)
    {
        return builder.ToHostBuilder();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ReturnsTheWebHostBuilder()
    {
        var builder = WebApplication.CreateBuilder();
        Assert.Same(builder.Host, new BuilderConverter().Convert(builder));
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="d565f9c40d015b247963fdee"><a href="~/run-actions/d565f9c40d015b247963fdee/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/d565f9c40d015b247963fdee/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
