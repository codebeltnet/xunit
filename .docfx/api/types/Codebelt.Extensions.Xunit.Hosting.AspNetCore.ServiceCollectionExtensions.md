---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.ServiceCollectionExtensions
example:
- *content
---

The following example registers a fake `IHttpContextAccessor` on an `IServiceCollection` so that unit tests can simulate HTTP context without hosting a real server. Call `AddFakeHttpContextAccessor` with the desired service lifetime to control how the accessor is reused across requests.

```csharp
using Codebelt.Extensions.Xunit.Hosting.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace WebFixtureTests;

public class ExampleFixture
{
    public IServiceCollection ConfigureServices(IServiceCollection services)
    {
        return services.AddFakeHttpContextAccessor(ServiceLifetime.Scoped);
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        using var provider = new ExampleFixture().ConfigureServices(new ServiceCollection()).BuildServiceProvider();
        using var scope = provider.CreateScope();
        Assert.IsType<Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.FakeHttpContextAccessor>(scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>());
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="f3c0856195727da12ff2925f"><a href="~/run-actions/f3c0856195727da12ff2925f/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/f3c0856195727da12ff2925f/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
