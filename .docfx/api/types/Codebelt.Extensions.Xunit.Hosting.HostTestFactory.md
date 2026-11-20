---
uid: Codebelt.Extensions.Xunit.Hosting.HostTestFactory
example:
- *content
---

Use `HostTestFactory` when the test owns the Generic Host setup instead of loading an existing application entry point. This example registers a worker-facing status service, creates a managed host context, and reads the configured value through the same service provider the hosted application would use.

```csharp
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace WorkerServices.Tests;

public sealed class HostTestFactoryExample
{
    public string ResolveWorkerStatus()
    {
        using var host = HostTestFactory.Create(
            services => services.AddSingleton(new WorkerStatus("ready")));

        var status = host.Host.Services.GetRequiredService<WorkerStatus>();
        return status.Value;
    }
}

public sealed record WorkerStatus(string Value);

public sealed class RunExample
{
    [Fact]
    public void ExercisesExample()
    {
        Assert.Equal("ready", new HostTestFactoryExample().ResolveWorkerStatus());
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="9cdeb08f8ba8f3b361a98c58"><a href="~/run-actions/9cdeb08f8ba8f3b361a98c58/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/9cdeb08f8ba8f3b361a98c58/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
