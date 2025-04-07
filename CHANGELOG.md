# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

For more details, please refer to `PackageReleaseNotes.txt` on a per assembly basis in the `.nuget` folder.

> [!NOTE]  
> Changelog entries prior to version 8.4.0 was migrated from previous versions of Cuemon.Extensions.Xunit, Cuemon.Extensions.Xunit.Hosting, and Cuemon.Extensions.Xunit.Hosting.AspNetCore.

## [10.0.0] - TBD

This major release introduces support for unit testing Minimal APIs and includes numerous breaking changes with valuable learnings from previous 9.0.x releases. These changes aim to ensure greater consistency across the `Codebelt.Extensions.Xunit.Hosting` and `Codebelt.Extensions.Xunit.Hosting.AspNetCore` namespaces.

### Added

- HostTest class in the Codebelt.Extensions.Xunit.Hosting namespace that represents the non-generic base class from where its generic equivalent should derive (e.g., MinimalHostTest{T}, HostTest{T}, etc.)
- IGenericHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace that provides a set of members for configuring the host
- GenericHostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace that provides a default implementation of the IGenericHostFixture interface
- GenericHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of one extension method for the IGenericHostFixture interface: HasValidState
- IMinimalHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace that provides a set of members for configuring the host (minimal style)
- MinimalHostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace that provides a default implementation of the IMinimalHostFixture interface
- MinimalHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of one extension method for the IMinimalHostFixture interface: HasValidState
- MinimalHostTest class in the Codebelt.Extensions.Xunit.Hosting namespace that represents the non-generic base class from where its generic equivalent should derive (e.g., MinimalWebHostTest, {T}, MinimalHostTest{T}, etc.)
- MinimalHostTestFactory class in the Codebelt.Extensions.Xunit.Hosting namespace that provides a set of static methods for IHost unit testing (minimal style)
- HostTest class in the Codebelt.Extensions.Xunit.Hosting namespace that represents the non-generic base class from where its generic equivalent should derive (e.g., MinimalHostTest{T}, HostTest{T}, etc.)
- IGenericHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace that provides a set of members for configuring the host
- GenericHostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace that provides a default implementation of the IGenericHostFixture interface
- GenericHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of one extension method for the IGenericHostFixture interface: HasValidState
- IMinimalHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace that provides a set of members for configuring the host (minimal style)
- MinimalHostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace that provides a default implementation of the IMinimalHostFixture interface
- MinimalHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of one extension method for the IMinimalHostFixture interface: HasValidState
- MinimalHostTest class in the Codebelt.Extensions.Xunit.Hosting namespace that represents the non-generic base class from where its generic equivalent should derive (e.g., MinimalWebHostTest, {T}, MinimalHostTest{T}, etc.)
- MinimalHostTestFactory class in the Codebelt.Extensions.Xunit.Hosting namespace that provides a set of static methods for IHost unit testing (minimal style)

### Changed

- IHostingEnvironmentTest in the Codebelt.Extensions.Xunit.Hosting namespace was renamed to IEnvironmentTest (breaking change)
- GenericHostTestFactory in the Codebelt.Extensions.Xunit.Hosting namespace was renamed to HostTestFactory (breaking change)
- IGenericHostTest in the Codebelt.Extensions.Xunit.Hosting namespace was renamed to IHostTest (breaking change)
- HostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace was changed to an abstract class from which all other host fixture classes derive from (e.g., WebHostFixture, GenericHostFixture, etc.)
- IHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace was changed to be more confined in scope (e.g., less interface inheritance and ultimately fewer members)
- AspNetCoreHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace was renamed to WebHostFixture (breaking change)
- AspNetCoreHostTest class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace was renamed to WebHostTest (breaking change)
- BlockingAspNetCoreHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace was renamed to BlockingWebHostFixture (breaking change)
- IAspNetCoreHostFixture interface in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace was renamed to IWebHostFixture (breaking change)

### Removed

- IServiceTest interface in the Codebelt.Extensions.Xunit.Hosting namespace due to redundancies with the IHost interface (Services property) (breaking change)
- HostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace (breaking change)
- AspNetCoreHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace (breaking change)

## [9.1.3] - 2025-04-03

### Fixed

- IGenericHostTest interface in the Codebelt.Extensions.Xunit.Hosting namespace to include the IHostTest interface

### Changed

- HostTest class in the Codebelt.Extensions.Xunit.Hosting namespace to include the IGenericHostTest interface
- AspNetCoreHostTest class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace to include the IWebHostTest interface

## [9.1.2] - 2025-04-03

### Fixed

- HostTest class in the Codebelt.Extensions.Xunit.Hosting namespace to have same behavior as prior to `9.1.0` release (hereby being backward compatible as originally intended)
  - Reintroduced `Configure` method to be virtual (brain fart; should have been captured with `9.1.1` release)

## [9.1.1] - 2025-04-01

### Added

- LoggerExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace received one new extension method for the ILogger interface: An overload of GetTestStore that takes an optional string argument (categoryName)

### Changed

- HostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace so that IHostEnvironment.ApplicationName is aligned with the equivalent logic found in AspNetCoreHostFixture class (e.g., the assembly name of the calling Test type is used as the default value for the ApplicationName property)

### Fixed

- HostTest class in the Codebelt.Extensions.Xunit.Hosting namespace to have same behavior as prior to `9.1.0` release (hereby being backward compatible as originally intended)
- LoggerExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace to have same behavior as prior to `9.1.0` release (hereby being backward compatible as originally intended)
- AspNetCoreHostTest class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace to have same behavior as prior to 9.1.0 release (hereby being backward compatible as originally intended)

## [9.1.0] - 2025-03-31

This is a service update that primarily focuses on package dependencies including DIP improvements and a new blocking implementation of the AspNetCoreHostFixture.

> [!WARNING]
> Although this release is backward compatible, do expect some design-time incompatibility due to changes in `GenericHostTestFactory` and `WebHostTestFactory`.

### Added

- HostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of one extension method for the IHostFixture interface: HasValidState
- AspNetCoreHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that consist of one extension method for the IAspNetCoreHostFixture interface: HasValidState
- BlockingAspNetCoreHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that provides a blocking implementation of the AspNetCoreHostFixture implementation

### Changed

- HostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace to have an additional virtual method: StartConfiguredHost, which is called from the ConfigureHost method, to allow for custom implementations of the host startup process
- GenericHostTestFactory class in the Codebelt.Extensions.Xunit.Hosting namespace to accept an optional argument taking a custom implementation of IHostFixture (promote DIP)
- WebHostTestFactory class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace to accept an optional argument taking a custom implementation of IAspNetCoreHostFixture (promote DIP)

## [9.0.1] - 2025-01-25

This is a service update that primarily focuses on package dependencies and minor improvements.

> [!IMPORTANT]
> Dependencies used for targeting .NET Standard 2.0 has been updated to use .NET 8.0 (LTS) instead of .NET Core 2.1.

### Dependencies

- Codebelt.Extensions.Xunit updated to latest and greatest with respect to TFMs
- Codebelt.Extensions.Xunit.Hosting updated to latest and greatest with respect to TFMs
- Codebelt.Extensions.Xunit.Hosting.AspNetCore updated to latest and greatest with respect to TFMs

### Changed

- HostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace no longer have a dependency to IHostingEnvironment (TFM netstandard2.0)
- HostTest class in the Codebelt.Extensions.Xunit.Hosting namespace no longer have a dependency to IHostingEnvironment (TFM netstandard2.0)
- IHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace no longer have a dependency to IHostingEnvironment (TFM netstandard2.0)
- IHostingEnvironmentTest interface in the Codebelt.Extensions.Xunit.Hosting namespace no longer have a dependency to IHostingEnvironment (TFM netstandard2.0)

## [9.0.0] - 2024-11-13

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
