---
uid: Codebelt.Extensions.Xunit.Hosting.AspNetCore.ManagedWebApplicationFixture`1
example:
- *content
---

Use `ManagedWebApplicationFixture<TEntryPoint>` with `WebApplicationTest<TEntryPoint,TFixture>` when an xUnit test class needs the real ASP.NET Core entry point and an in-memory `TestServer`. In this excerpt, xUnit supplies the fixture to the constructor, and the base class configures it before the test accesses `Host`. The test project references the separate `Program.App` executable. A request to `/` verifies that the application resolves its registered marker and reports the Development environment.

```csharp
// docfx-context: test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests.csproj
// docfx-source: test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests/WebApplicationTestTest.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.Program.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ModernProgram = Codebelt.Extensions.Xunit.Hosting.Program.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class WebApplicationTestTest : WebApplicationTest<ModernProgram, ManagedWebApplicationFixture<ModernProgram>>
{
    public WebApplicationTestTest(ManagedWebApplicationFixture<ModernProgram> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public async Task ShouldBootstrapApplication_WhenEntryPointUsesModernProgramPattern()
    {
        using var client = Host.GetTestClient();

        var response = await client.GetAsync("/").ConfigureAwait(false);
        var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal("Modern Program|Development", body);
    }
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="4c5e94adccd20cab4da52078"><a href="~/run-actions/4c5e94adccd20cab4da52078/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/4c5e94adccd20cab4da52078/docker.ps1" data-transport="docker">View Docker Script</a><p>Runs the pinned repository test <code>Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebApplicationTestTest.ShouldBootstrapApplication_WhenEntryPointUsesModernProgramPattern</code>.</p><p>The scripts start the maintained ASP.NET Core application through the same fixture and verify its HTTP response.</p></div>
<!-- docfx-run-actions:end -->

The [maintained web application test](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests/WebApplicationTestTest.cs) uses the managed fixture with a separate ASP.NET Core entry-point assembly.

The [application entry point](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/app/Codebelt.Extensions.Xunit.Hosting.Program.App/Program.cs) defines the behavior the test exercises. The [functional test project](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests.csproj) references that executable project.
