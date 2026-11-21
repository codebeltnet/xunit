---
uid: Codebelt.Extensions.Xunit.Hosting.ApplicationTestFactory
example:
- *content
---

Use `ApplicationTestFactory.Create<TEntryPoint>` to test an existing worker application in an owned test context. The default `ManagedApplicationFixture<TEntryPoint>` lets the worker entry point own host startup. The excerpt below belongs to an xUnit test project that references the separate `BootstrapperMinimalWorker` application. It verifies the marker registered by the application, the Development environment, and the availability of `IHostApplicationLifetime`. Pass an explicit `IApplicationFixture<TEntryPoint>` when the test needs a different lifecycle.

```csharp
// docfx-context: test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.FunctionalTests.csproj
// docfx-source: test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/ApplicationTestFactoryTest.cs
    [Fact]
    public void Create_ShouldBootstrapApplication_WhenEntryPointUsesBootstrapperMinimalWorkerProgram()
    {
        using var application = ApplicationTestFactory.Create<BootstrapperMinimalWorkerProgram>();

        var marker = application.Host.Services.GetRequiredService<BootstrapperMinimalWorkerMarker>();

        Assert.Equal("Bootstrapper Minimal Worker", marker.Value);
        Assert.Equal("Development", application.Environment.EnvironmentName);
        Assert.NotNull(application.Host.Services.GetRequiredService<IHostApplicationLifetime>());
    }
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="90a8cd444bd08a0115613e58"><a href="~/run-actions/90a8cd444bd08a0115613e58/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/90a8cd444bd08a0115613e58/docker.ps1" data-transport="docker">View Docker Script</a><p>Runs the pinned repository test <code>Codebelt.Extensions.Xunit.Hosting.ApplicationTestFactoryTest.Create_ShouldBootstrapApplication_WhenEntryPointUsesBootstrapperMinimalWorkerProgram</code>.</p><p>The scripts run the maintained worker integration test and verify its registered marker and Development environment.</p></div>
<!-- docfx-run-actions:end -->

The [maintained worker test](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/ApplicationTestFactoryTest.cs) checks the same service resolution and environment against a worker in its own application project.

The [application entry point](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/app/Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App/Program.cs) defines the behavior the test exercises. The [functional test project](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.FunctionalTests.csproj) references that executable project.
