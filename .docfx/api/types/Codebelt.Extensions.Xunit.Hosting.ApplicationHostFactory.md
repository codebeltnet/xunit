---
uid: Codebelt.Extensions.Xunit.Hosting.ApplicationHostFactory
example:
- *content
---

Use `ApplicationHostFactory.Create<TEntryPoint>` when you need a built `IHost` and will own its disposal. This standalone console example uses its own executable as the entry-point assembly to demonstrate the compatibility path. The factory calls `CreateHostBuilder` directly, applies the service registration supplied by the caller, and builds the host. `Main` resolves and prints the configured worker identity. In a test of an existing worker, reference the application project and use its entry-point type as `TEntryPoint`. Use `ApplicationTestFactory.Create<TEntryPoint>` when the application entry point should own startup.

```csharp
using System;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerApp.Tests;

public sealed class ApplicationHostFactoryExample
{
    public string GetTestIdentity()
    {
        using IHost host = ApplicationHostFactory.Create<WorkerProgram>(builder =>
        {
            builder.ConfigureServices(services =>
                services.AddSingleton(new WorkerIdentity("Test inventory worker")));
        });

        var identity = host.Services.GetRequiredService<WorkerIdentity>();
        return identity.Name;
    }
}

public sealed record WorkerIdentity(string Name);

public sealed class WorkerProgram
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args);
    }

    public static void Main(string[] args)
    {
        var identity = new ApplicationHostFactoryExample().GetTestIdentity();
        if (identity != "Test inventory worker")
        {
            throw new InvalidOperationException(identity);
        }

        Console.WriteLine(identity);
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="ff2e361441bdc3028ab188a9"><a href="~/run-actions/ff2e361441bdc3028ab188a9/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/ff2e361441bdc3028ab188a9/docker.ps1" data-transport="docker">View Docker Script</a></div>
<!-- docfx-run-actions:end -->
