---
uid: Codebelt.Extensions.Xunit.Hosting.ServiceProviderExtensions
example:
- *content
---

The following example retrieves a scoped service of type `T` from an `IServiceProvider` by calling `GetRequiredScopedService<T>`. The extension creates a new scope, resolves the service from the scope's provider, and disposes the scope before returning.

```csharp
using System;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HostFixtureTests;

public class ServiceResolver
{
    public T Resolve<T>(IServiceProvider provider) where T : notnull
    {
        return provider.GetRequiredScopedService<T>();
    }
}

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        var services = new ServiceCollection();
        services.AddScoped(_ => new object());
        using var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true
        });
        Assert.NotSame(new ServiceResolver().Resolve<object>(provider), new ServiceResolver().Resolve<object>(provider));
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="5e35a33e56470f93afbedf19"><a href="~/run-actions/5e35a33e56470f93afbedf19/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/5e35a33e56470f93afbedf19/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
