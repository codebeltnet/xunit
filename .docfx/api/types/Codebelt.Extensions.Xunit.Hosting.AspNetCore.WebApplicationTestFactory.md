---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebApplicationTestFactory
example:
- *content
---

Use `WebApplicationTestFactory.Create<TEntryPoint>` to exercise an existing ASP.NET Core application through `TestServer`. The default `ManagedWebApplicationFixture<TEntryPoint>` runs the entry point from the separate `Program.App` project. In this excerpt from the functional test, the web-host setup supplies configuration and registers a service. Requests to `/configuration` and `/custom-service` verify that the application endpoints see both changes. The test disposes the client and the factory context.

```csharp
// docfx-context: test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests.csproj
// docfx-source: test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests/WebApplicationTestFactoryTest.cs
    [Fact]
    public async Task Create_ShouldApplyWebHostConfiguration_WhenWebHostSetupIsProvided()
    {
        using var application = WebApplicationTestFactory.Create<ModernProgram>(builder =>
        {
            builder.ConfigureAppConfiguration((_, configuration) =>
            {
                configuration.AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["ProgramLane:Message"] = "Configured from WebApplicationTestFactory"
                });
            });

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(new ProgramCustomization("Custom service from WebApplicationTestFactory"));
            });
        });
        using var client = application.Host.GetTestClient();

        var configurationResponse = await client.GetAsync("/configuration").ConfigureAwait(false);
        var configurationBody = await configurationResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
        var serviceResponse = await client.GetAsync("/custom-service").ConfigureAwait(false);
        var serviceBody = await serviceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(configurationResponse.IsSuccessStatusCode);
        Assert.Equal("Configured from WebApplicationTestFactory", configurationBody);
        Assert.True(serviceResponse.IsSuccessStatusCode);
        Assert.Equal("Custom service from WebApplicationTestFactory", serviceBody);
    }
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="38b60f4b4c1a588bbefe92f1"><a href="~/run-actions/38b60f4b4c1a588bbefe92f1/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/38b60f4b4c1a588bbefe92f1/docker.ps1" data-transport="docker">View Docker Script</a><p>Runs the pinned repository test <code>Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebApplicationTestFactoryTest.Create_ShouldApplyWebHostConfiguration_WhenWebHostSetupIsProvided</code>.</p><p>The scripts run the maintained integration test, which overrides services and configuration and verifies the results through HTTP endpoints.</p></div>
<!-- docfx-run-actions:end -->

The [maintained factory test](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests/WebApplicationTestFactoryTest.cs) shows how service and configuration overrides reach the application endpoints.

The [application entry point](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/app/Codebelt.Extensions.Xunit.Hosting.Program.App/Program.cs) defines the behavior the test exercises. The [functional test project](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests.csproj) references that executable project.
