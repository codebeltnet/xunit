# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

For more details, please refer to `PackageReleaseNotes.txt` on a per assembly basis in the `.nuget` folder.

> [!NOTE]  
> Changelog entries prior to version 8.4.0 was migrated from previous versions of Cuemon.Extensions.Xunit, Cuemon.Extensions.Xunit.Hosting, and Cuemon.Extensions.Xunit.Hosting.AspNetCore.

## [9.0.0] - TBD

This major release is first and foremost focused on ironing out any wrinkles that have been introduced with .NET 9 preview releases so the final release is production ready together with the official launch from Microsoft.

### Added

- StringExtensions class in the Codebelt.Extensions.Xunit namespace with one extension method (TFM netstandard2.0) for the String class: ReplaceLineEndings
- ITest interface in the Codebelt.Extensions.Xunit namespace was extended with one new method: DisposeAsync
- Test class in the Codebelt.Extensions.Xunit namespace was extended with three new methods: InitializeAsync, OnDisposeManagedResourcesAsync and DisposeAsync
- IHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace was extended with two new methods: Dispose and DisposeAsync
- HostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace was extended with three new methods: InitializeAsync, OnDisposeManagedResourcesAsync, Dispose and DisposeAsync

### Changed

- HostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace to enable `ValidateOnBuild` and `ValidateScopes` when TFM is .NET 9 (or greater) and started the Host for consistency with AspNetCoreHostFixture
- FakeHttpContextAccessor class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http namespace to support IServiceProvidersFeature (e.g., `RequestServices` property will be available for consumption by tests)
- ServiceCollectionExtensions class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace to have AddFakeHttpContextAccessor `lifetime` argument as optional with a default value of `ServiceLifetime.Singleton`

### Removed

- AddXunitTestLogging overloaded extension method from the ServiceCollectionExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace (breaking)

### Fixed

- AspNetCoreHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace to preserve ExecutionContext and AsyncLocal{T} values from the client to the server (vital for ITestOutputHelperAccessor combined with xUnit test logging when using HttpClient)
  - Prior to this release, you can override `ConfigureHost` on your `AspNetCoreHostTest` implementation and apply this code:
    ```csharp
    protected override void ConfigureHost(IHostBuilder hb)
    {
        hb.ConfigureWebHost(builder => builder.UseTestServer(o => o.PreserveExecutionContext = true));
    }
    ```
- AspNetCoreHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace to only enable `ValidateOnBuild` and `ValidateScopes` when TFM is .NET 9 (or greater)

## [8.4.1] - 2024-09-16

### Added

- ServiceCollectionExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace received one new extension method for the IServiceCollection interface: An overload of AddXunitTestLogging

### Changed

- AddXunitTestOutputHelperAccessor and AddXunitTestOutputHelperAccessor{T} on the ServiceCollectionExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace was renamed to AddXunitTestLoggingOutputHelperAccessor and AddXunitTestLoggingOutputHelperAccessor{T} (for consistency)

## [8.4.0] - 2024-09-15

### Added

- HttpClientExtensions class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that consist of one extension method for the HttpClient class: ToHttpResponseMessageAsync
- ITestOutputHelperAccessor interface in the Codebelt.Extensions.Xunit namespace that provides access to the ITestOutputHelper instance
- TestOutputHelperAccessor class in the Codebelt.Extensions.Xunit namespace that provides a default implementation of the ITestOutputHelper interface
- ServiceProviderExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of one extension method for the IServiceProvider interface: GetRequiredScopedService
- ServiceCollectionExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace received two new extension methods for the IServiceCollection interface: AddXunitTestOutputHelperAccessor and AddXunitTestOutputHelperAccessor{T}

### Changed

- AspNetCoreHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace to use same [hostbuilder validation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/aspnet-core/9.0/hostbuilder-validation) as introduced with .NET preview 7
- Run method on the WebHostTestFactory class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore was renamed to RunAsync (breaking change)
- RunWithHostBuilderContext method on the WebHostTestFactory class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore was renamed to RunWithHostBuilderContextAsync (breaking change)

### Removed

- Codebelt.Extensions.Xunit.Hosting.AspNetCore.Mvc project due to redundancies with Codebelt.Extensions.Xunit.Hosting.AspNetCore (breaking change)
- IMiddlewareTest interface from the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace (breaking change)
- MiddlewareTestFactory static class from the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace (breaking change)

## [8.3.2] - 2024-08-04

### Dependencies

- Codebelt.Extensions.Xunit updated to latest and greatest with respect to TFMs
- Codebelt.Extensions.Xunit.Hosting updated to latest and greatest with respect to TFMs
- Codebelt.Extensions.Xunit.Hosting.AspNetCore updated to latest and greatest with respect to TFMs
- Codebelt.Extensions.Xunit.Hosting.AspNetCore.Mvc updated to latest and greatest with respect to TFMs

### Removed

- TFM net7.0 for all projects due to [EOL](https://endoflife.date/dotnet)

## [8.3.1] - 2024-06-01

### Dependencies

- Codebelt.Extensions.Xunit updated to latest and greatest with respect to TFMs
- Codebelt.Extensions.Xunit.Hosting updated to latest and greatest with respect to TFMs
- Codebelt.Extensions.Xunit.Hosting.AspNetCore updated to latest and greatest with respect to TFMs
- Codebelt.Extensions.Xunit.Hosting.AspNetCore.Mvc updated to latest and greatest with respect to TFMs

### Added

- IWebHostTest interface in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that represents the members needed for ASP.NET Core (including but not limited to MVC, Razor and related) testing
- WebHostTestFactory class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that provides a set of static methods for ASP.NET Core (including, but not limited to MVC, Razor and related) unit testing

### Deprecated

- IMiddlewareTest interface in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace; use the consolidated IWebHostTest instead
- MiddlewareTestFactory class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace; use the consolidated WebHostTestFactory instead
- IWebApplicationTest interface in the Codebelt.Extensions.Xunit.Hosting.AspNetCore.Mvc namespace; use the consolidated IWebHostTest in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace instead
- WebApplicationTestFactory class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore.Mvc namespace; use the consolidated WebHostTestFactory in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace instead

## [8.3.0] - 2024-04-09

### Added

- Test class in the Codebelt.Extensions.Xunit namespace was extended with one new static method: Match
- WildcardOptions class in the Codebelt.Extensions.Xunit namespace that provides configuration options for the Match method on the Test class

## [8.1.0] - 2024-02-11

### Added

- LoggerExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of extension methods for the ILogger{T} interface: GetTestStore{T}
- ServiceCollectionExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of extension methods for the IServiceCollection interface: AddXunitTestLogging
- TestLoggerEntry record in the Codebelt.Extensions.Xunit.Hosting namespace that represents a captured log-entry for testing purposes

## [8.0.0] - 2023-11-14

### Changed

- Extended unit-test to include TFM net8.0, net7.0, net6.0 and net48 for Windows
  - Had to include Microsoft.TestPlatform.ObjectModel for xUnit when testing on legacy .NET Framework

### Fixed

- Added null conditional operator to the ServiceProvider property on the HostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace
