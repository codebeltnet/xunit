---
uid: Codebelt.Extensions.Xunit.Hosting.ManagedApplicationFixture`1
example:
- *content
---

Use `ManagedApplicationFixture<TEntryPoint>` when an xUnit test should run the application's real entry point and inspect services registered by that application. `TEntryPoint` identifies the executable application assembly; keep its `Program` and startup registrations in the application project, separate from the xUnit runner assembly. Derive the test from `ApplicationTest<TEntryPoint,TFixture>` and inject the fixture so the base class initializes it before the test accesses `Host`.

The maintained worker test below resolves a marker registered by the worker's real `Program` and checks the marker value, Development environment, and host lifetime service. Its test project references the worker application as a separate executable project, preserving the assembly identity required by `ManagedApplicationFixture<TEntryPoint>`.

```csharp
// docfx-context: test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.FunctionalTests.csproj
// docfx-source: test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/BootstrapperMinimalWorkerApplicationTestTest.cs
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using BootstrapperMinimalWorkerMarker = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App.BootstrapperMinimalWorkerMarker;
using BootstrapperMinimalWorkerProgram = Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting;

public class BootstrapperMinimalWorkerApplicationTestTest : ApplicationTest<BootstrapperMinimalWorkerProgram, ManagedApplicationFixture<BootstrapperMinimalWorkerProgram>>
{
    public BootstrapperMinimalWorkerApplicationTestTest(ManagedApplicationFixture<BootstrapperMinimalWorkerProgram> hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
    }

    [Fact]
    public void ShouldBootstrapMinimalWorkerProgram()
    {
        var marker = Host.Services.GetRequiredService<BootstrapperMinimalWorkerMarker>();

        Assert.Equal("Bootstrapper Minimal Worker", marker.Value);
        Assert.Equal("Development", Environment.EnvironmentName);
        Assert.NotNull(Host.Services.GetRequiredService<IHostApplicationLifetime>());
    }
}
```

<!-- docfx-run-actions:start -->
<div class="docfx-run-actions" data-example-id="1ec9196292addaca42a7eef1"><a href="~/run-actions/1ec9196292addaca42a7eef1/native.ps1" data-transport="native">View Native Script</a> · <a href="~/run-actions/1ec9196292addaca42a7eef1/docker.ps1" data-transport="docker">View Docker Script</a><p>Runs the pinned repository test <code>Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorkerApplicationTestTest.ShouldBootstrapMinimalWorkerProgram</code>.</p><p>The scripts run this test in its original application and test projects.</p></div>
<!-- docfx-run-actions:end -->

To reproduce this exact test without assembling its multi-project context, run either complete PowerShell 7 option below. Each script downloads the same pinned repository revision and runs the maintained test without creating a substitute application or changing its assertions; the test verifies one passed result and checks the runner report for the exact test name.

## Native: PowerShell 7 and .NET 11 SDK

```powershell
#requires -Version 7.0
& {
    $ErrorActionPreference = 'Stop'
    $PSNativeCommandUseErrorActionPreference = $false
    # Executes trusted repository build tasks and NuGet packages with your permissions.
    # Review the pinned source before running; a container is not a security sandbox.
    $null = Get-Command dotnet -ErrorAction Stop
    $work = Join-Path ([IO.Path]::GetTempPath()) ('dx-' + [guid]::NewGuid().ToString('N').Substring(0, 8))
    $null = New-Item -ItemType Directory -Path $work
    try {
        $commit = '84b13767cb9484f8f3a36a71875662edfdb3e511'
        Invoke-WebRequest "https://codeload.github.com/codebeltnet/xunit/zip/$commit" -OutFile "$work/source.zip"
        Expand-Archive -LiteralPath "$work/source.zip" -DestinationPath "$work/extracted"
        Move-Item -LiteralPath "$work/extracted/xunit-$commit" -Destination "$work/repo"
        $project = 'test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.FunctionalTests.csproj'
        $test = 'Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorkerApplicationTestTest.ShouldBootstrapMinimalWorkerProgram'
        Push-Location "$work/repo"
        try {
            dotnet run --project $project --framework net11.0 -p:SkipSignAssembly=true -- --filter-method $test --report-xunit-xml --report-xunit-xml-filename result.xml --results-directory "$work/results" 2>&1 | Tee-Object -FilePath "$work/run.log"
        }
        finally { Pop-Location }
        $code = $LASTEXITCODE
        if ($code -ne 0) { throw "Repository test failed with exit code $code. See $work/run.log" }
        # Require exactly the original test, executed and passing, with no runner errors.
        [xml] $report = Get-Content -LiteralPath "$work/results/result.xml" -Raw
        $tests = @($report.SelectNodes('/assemblies/assembly/collection/test'))
        $assemblies = @($report.SelectNodes('/assemblies/assembly'))
        if ($assemblies.Count -ne 1 -or $assemblies[0].total -ne '1' -or $assemblies[0].passed -ne '1' -or
            $assemblies[0].errors -ne '0' -or $tests.Count -ne 1 -or $tests[0].name -cne $test -or $tests[0].result -cne 'Pass') {
            throw "Expected exactly one passing $test; inspect $work/results/result.xml"
        }
        Write-Host "Verified: $test (1 executed, 1 passed)."
    }
    catch {
        # Preserve diagnostics outside the repository even though downloaded source is cleaned.
        $diagnostics = "$work-diagnostics"
        $null = New-Item -ItemType Directory -Path $diagnostics -Force
        foreach ($name in @('run.log', 'results')) {
            if (Test-Path -LiteralPath "$work/$name") { Copy-Item -LiteralPath "$work/$name" -Destination $diagnostics -Recurse }
        }
        Write-Warning "Execution not verified. Diagnostics: $diagnostics"
        throw
    }
    finally {
        $resolved = [IO.Path]::GetFullPath($work)
        $temp = [IO.Path]::GetFullPath([IO.Path]::GetTempPath()).TrimEnd('/','\') + [IO.Path]::DirectorySeparatorChar
        if (-not $resolved.StartsWith($temp, [StringComparison]::OrdinalIgnoreCase)) { throw 'Unsafe cleanup path.' }
        Remove-Item -LiteralPath $resolved -Recurse -Force
    }
}
```

## Docker: PowerShell 7 and Docker with Linux containers

No host .NET SDK is required. The official SDK image supplies it; first use pulls the image. The `11.0` image tag can move as Microsoft publishes SDK updates. On Linux, the container runs with the host UID and GID so build and test outputs remain removable during cleanup. Its home and .NET CLI state use container-local `/tmp`, which is writable by that identity.

```powershell
#requires -Version 7.0
& {
    $ErrorActionPreference = 'Stop'
    $PSNativeCommandUseErrorActionPreference = $false
    # Executes trusted repository build tasks and NuGet packages with your permissions.
    # Review the pinned source before running; a container is not a security sandbox.
    $null = Get-Command docker -ErrorAction Stop
    docker info *> $null
    if ($LASTEXITCODE -ne 0) { throw 'Docker daemon unavailable.' }
    $work = Join-Path ([IO.Path]::GetTempPath()) ('dx-' + [guid]::NewGuid().ToString('N').Substring(0, 8))
    $null = New-Item -ItemType Directory -Path $work
    try {
        $commit = '84b13767cb9484f8f3a36a71875662edfdb3e511'
        Invoke-WebRequest "https://codeload.github.com/codebeltnet/xunit/zip/$commit" -OutFile "$work/source.zip"
        Expand-Archive -LiteralPath "$work/source.zip" -DestinationPath "$work/extracted"
        Move-Item -LiteralPath "$work/extracted/xunit-$commit" -Destination "$work/repo"
        $project = 'test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.FunctionalTests.csproj'
        $test = 'Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorkerApplicationTestTest.ShouldBootstrapMinimalWorkerProgram'
        $dockerUser = @()
        if ($IsLinux) {
            # Keep bind-mounted build output removable by the host user.
            $hostUid = id -u
            if ($LASTEXITCODE -ne 0) { throw 'Cannot resolve host UID.' }
            $hostGid = id -g
            if ($LASTEXITCODE -ne 0) { throw 'Cannot resolve host GID.' }
            $dockerUser = @('--user', "${hostUid}:${hostGid}")
        }
        docker run --rm @dockerUser --env HOME=/tmp --env DOTNET_CLI_HOME=/tmp --mount "type=bind,source=$work,target=/work" --workdir /work/repo mcr.microsoft.com/dotnet/sdk:11.0 dotnet run --project $project --framework net11.0 -p:SkipSignAssembly=true -- --filter-method $test --report-xunit-xml --report-xunit-xml-filename result.xml --results-directory /work/results 2>&1 | Tee-Object -FilePath "$work/run.log"
        $code = $LASTEXITCODE
        if ($code -ne 0) { throw "Repository test failed with exit code $code. See $work/run.log" }
        # Require exactly the original test, executed and passing, with no runner errors.
        [xml] $report = Get-Content -LiteralPath "$work/results/result.xml" -Raw
        $tests = @($report.SelectNodes('/assemblies/assembly/collection/test'))
        $assemblies = @($report.SelectNodes('/assemblies/assembly'))
        if ($assemblies.Count -ne 1 -or $assemblies[0].total -ne '1' -or $assemblies[0].passed -ne '1' -or
            $assemblies[0].errors -ne '0' -or $tests.Count -ne 1 -or $tests[0].name -cne $test -or $tests[0].result -cne 'Pass') {
            throw "Expected exactly one passing $test; inspect $work/results/result.xml"
        }
        Write-Host "Verified: $test (1 executed, 1 passed)."
    }
    catch {
        # Preserve diagnostics outside the repository even though downloaded source is cleaned.
        $diagnostics = "$work-diagnostics"
        $null = New-Item -ItemType Directory -Path $diagnostics -Force
        foreach ($name in @('run.log', 'results')) {
            if (Test-Path -LiteralPath "$work/$name") { Copy-Item -LiteralPath "$work/$name" -Destination $diagnostics -Recurse }
        }
        Write-Warning "Execution not verified. Diagnostics: $diagnostics"
        throw
    }
    finally {
        $resolved = [IO.Path]::GetFullPath($work)
        $temp = [IO.Path]::GetFullPath([IO.Path]::GetTempPath()).TrimEnd('/','\') + [IO.Path]::DirectorySeparatorChar
        if (-not $resolved.StartsWith($temp, [StringComparison]::OrdinalIgnoreCase)) { throw 'Unsafe cleanup path.' }
        Remove-Item -LiteralPath $resolved -Recurse -Force
    }
}
```

## Further exploration

The [pinned worker entry point](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/app/Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalWorker.App/Program.cs) constructs the host, registers the marker and hosted service, and runs the application. The [functional test source](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/BootstrapperMinimalWorkerApplicationTestTest.cs) shows the same fixture usage in its maintained class, while the [test project](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/test/Codebelt.Extensions.Xunit.Hosting.FunctionalTests/Codebelt.Extensions.Xunit.Hosting.FunctionalTests.csproj) records its separate application and library references and target frameworks. The [fixture implementation](https://github.com/codebeltnet/xunit/blob/84b13767cb9484f8f3a36a71875662edfdb3e511/src/Codebelt.Extensions.Xunit.Hosting/ManagedApplicationFixture.cs) shows how it selects the entry-point assembly and connects deferred host startup.

The Native execution block was verified on Windows x64 with .NET SDK 11.0.100 and runtime 11.0.0; it reported the named test passing. The Docker block's most recent end-to-end attempt observed the test pass but the container exited with code 137, so Docker execution remains unverified.
