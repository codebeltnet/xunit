# Changelog

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/), and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

For more details, please refer to `PackageReleaseNotes.txt` on a per assembly basis in the `.nuget` folder.

> [!NOTE]
> Changelog entries prior to version 8.4.0 was migrated from previous versions of Cuemon.Extensions.Xunit, Cuemon.Extensions.Xunit.Hosting, and Cuemon.Extensions.Xunit.Hosting.AspNetCore.

## [11.2.0] - 2026-08-03

This is a minor release that adds opt-in entrypoint-owned startup for application and ASP.NET Core tests, while keeping the existing factory paths compatible for the current minor release and marking the older blocking fixtures as obsolete.

### Added

- `ManagedApplicationFixture<TEntryPoint>` and `ManagedWebApplicationFixture<TEntryPoint>` for opt-in entrypoint-owned startup in application and ASP.NET Core tests,
- Regression coverage for classic, minimal, Bootstrapper, worker, and modern ASP.NET Core entry points.

### Changed

- `HostTest`, `WebApplicationTest`, `ApplicationHostFactory`, and `WebApplicationTestFactory` now support lazy entrypoint-owned startup when the managed fixtures are used, while the existing factory paths preserve their previous startup behavior for the current minor release,
- Package README and release-note guidance were updated to explain when to choose the managed fixtures versus the compatibility paths.

### Deprecated

- `BlockingManagedApplicationFixture<TEntryPoint>` and `BlockingManagedWebApplicationFixture<TEntryPoint>` remain available for source and binary compatibility but are obsolete for new tests; they should be removed or changed in the next major release.

## [11.1.2] - 2026-07-18

This is a patch release that updates package dependencies across all supported target frameworks and introduces query performance benchmarks for the InMemoryTestStore implementation. All changes are non-breaking service refinements.

### Added

- InMemoryTestStoreBenchmark class in the tuning folder with 7 query performance methods covering null-predicate fast path, selective filtering at varying selectivity rates, type filtering with QueryFor<T>, and materialization cost measurement across workload sizes of 10, 100, and 1000 items,
- Query performance analysis reports documenting benchmark results across .NET 10.0, 9.0, and 4.8 runtimes.

### Changed

- Upgraded Codebelt.Bootstrapper.Console, Codebelt.Bootstrapper.Web, and Codebelt.Bootstrapper.Worker from 5.1.1 to 5.1.2,
- Upgraded Codebelt.Extensions.BenchmarkDotNet.Console from 1.3.0 to 1.3.1,
- Upgraded Cuemon.Core, Cuemon.Extensions.AspNetCore, and Cuemon.Extensions.IO from 10.5.4 to 10.5.5,
- Upgraded Microsoft.NET.Test.Sdk from 18.7.0 to 18.8.1,
- Upgraded Microsoft.Bcl.AsyncInterfaces from 10.0.9 to 10.0.10 for netstandard2.0,
- Upgraded Microsoft.Extensions.* packages (Configuration, EnvironmentVariables, FileExtensions, Json, Hosting, AspNetCore.TestHost) from 9.0.17 to 9.0.18 for .NET 9,
- Upgraded Microsoft.Extensions.* packages (Configuration, EnvironmentVariables, FileExtensions, Json, Hosting, AspNetCore.TestHost) from 10.0.9 to 10.0.10 for .NET 10.

## [11.1.1] - 2026-06-25

This is a patch release that improves documentation clarity, enhances DocFX infrastructure, and updates test framework dependencies. All changes are non-breaking service refinements.

### Added

- Prohibition on ExcludeFromCodeCoverage attribute usage in AGENTS.md agent guidance documentation,
- DocFX documentation workflow section in AGENTS.md documenting the complete maintenance and verification process for public API documentation.

### Changed

- Upgraded Microsoft.NET.Test.Sdk from 18.6.0 to 18.7.0 for improved test framework capabilities,
- Updated NGINX base image and coordinated package version updates for improved compatibility,
- Expanded DocFX namespace pages with improved entry-point guidance, usage patterns, and extension member tables,
- Configured DocFX build system to include newly generated type-level overwrite files for comprehensive public API documentation,
- Enhanced Hosting package README with clearer usage guidance for entry-point factory patterns and fixture options,
- Enhanced AspNetCore package README to clarify lightweight positioning versus WebApplicationFactory and decision criteria.

### Fixed

- Corrected ApplicationHostFactory XML documentation comments to accurately reflect that methods create and configure the host without starting it,
- Fixed changelog fixture descriptions to accurately state that BlockingManagedApplicationFixture and BlockingManagedWebApplicationFixture provide blocking implementation,
- Fixed DocFX overwrite glob pattern configuration to properly resolve api/namespaces/**/*.md files.

## [11.1.0] - 2026-06-05

This is a minor release that brings WebApplicationFactory-like integration testing patterns to the entire .NET application stack — not just ASP.NET Core. ApplicationHostFactory and ApplicationTest abstractions enable Program.cs-based testing for Generic Host scenarios, while WebApplicationTest provides the equivalent TestServer experience for ASP.NET Core. Both patterns support modern minimal hosting and legacy Startup.cs configurations, with comprehensive bootstrapper reference applications and functional test coverage demonstrating real-world testing scenarios.

### Added

- ApplicationHostFactory class in the Codebelt.Extensions.Xunit.Hosting namespace that creates started IHost instances from Program.cs entry points,
- ApplicationTest{TEntryPoint,T} base classes in the Codebelt.Extensions.Xunit.Hosting namespace for host integration testing patterns with generic and non-generic variants,
- ApplicationTestFactory class in the Codebelt.Extensions.Xunit.Hosting namespace for static factory methods to create host test instances,
- IApplicationFixture{TEntryPoint} interface in the Codebelt.Extensions.Xunit.Hosting namespace for fixture-based host lifecycle management,
- BlockingManagedApplicationFixture{TEntryPoint} class in the Codebelt.Extensions.Xunit.Hosting namespace providing blocking host fixture implementation,
- ApplicationFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace providing convenient fixture setup methods,
- WebApplicationTest{TEntryPoint,T} base classes in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace for ASP.NET Core Program.cs-based TestServer testing,
- WebApplicationTestFactory class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace for static factory methods to create web application test instances,
- IWebApplicationFixture{TEntryPoint} interface in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace for web application fixture lifecycle management,
- BlockingManagedWebApplicationFixture{TEntryPoint} class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace providing blocking web fixture implementation,
- WebApplicationFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace providing convenient web fixture setup methods,
- Eight bootstrapper reference applications demonstrating host patterns: BootstrapperConsole.App (classic Startup pattern), BootstrapperMinimalConsole.App (minimal hosting), BootstrapperWorker.App (BackgroundService with Startup), BootstrapperMinimalWorker.App (minimal worker service), BootstrapperWeb.App (ASP.NET Core with Startup), BootstrapperMinimalWeb.App (ASP.NET Core minimal), BootstrapperClassicProgram.App (top-level statements), and BootstrapperProgram.App (advanced customization),
- Comprehensive functional test coverage for hosting abstractions and integration patterns across Generic Host and ASP.NET Core scenarios, including all bootstrapper configurations.

### Changed

- Dependencies upgraded to latest compatible versions: added Codebelt.Bootstrapper.Console (5.1.0), Codebelt.Bootstrapper.Web (5.1.0), and Codebelt.Bootstrapper.Worker (5.1.0) packages; upgraded Codebelt.Extensions.BenchmarkDotNet.Console from 1.2.6 to 1.2.7; upgraded Microsoft.NET.Test.Sdk from 18.5.1 to 18.6.0,
- Solution structure reorganized with new /app/ folder containing eight bootstrapper reference applications,
- Project configuration updated with new Codebelt.Extensions.Xunit.Hosting.FunctionalTests and Codebelt.Extensions.Xunit.Hosting.AspNetCore.FunctionalTests functional test projects,
- Package release notes for all previous versions updated with consistent availability format information.

## [11.0.10] - 2026-05-21

This is a patch release focused on codebase modernization, enhanced testing coverage, and developer workflow improvements.

### Changed

- Refactored entire codebase across all three assemblies to use file-scoped namespaces for consistency with modern C# style conventions,
- Updated developer instructions in `.github/copilot-instructions.md` and `AGENTS.md` to mandate file-scoped namespaces throughout the project,
- Upgraded NGINX base image in documentation build Dockerfile to version 1.31.0-alpine for improved security and stability,
- Normalized whitespace and line endings across dependency configuration and package metadata files for consistency.

### Added

- Comprehensive unit tests for ASP.NET Core hosting fixtures including BlockingManagedWebHostFixture, SelfManagedWebHostFixture, SelfManagedWebMinimalHostFixture, HostBuilderApplicationExtensions, and FakeHttpResponseFeature to ensure robust hosting infrastructure,
- Unit test coverage for HTTP response feature mocking and request/response handling.

### Fixed

- Updated package README examples to reflect current API usage patterns with ManagedHostFixture and correct service provider access,
- Corrected GitHub repository references in package documentation to point to the codebeltnet organization namespace.

## [11.0.9] - 2026-04-16

This is a service update that focuses on package dependencies.

## [11.0.8] - 2026-03-13

This is a service update that focuses on package dependencies.

## [11.0.7] - 2026-02-27

This is a service update that focuses on package dependencies.

## [11.0.6] - 2026-02-20

This is a service update that focuses on package dependencies.

## [11.0.5] - 2026-02-15

This is a service update that focuses on package dependencies.

## [11.0.4] - 2026-01-20

This is a service update that focuses on package dependencies.

## [11.0.3] - 2025-12-15

This is a service update that focuses on package dependencies and performance improvements.

## [11.0.2] - 2025-12-06

This is a service update that focuses on package dependencies.

## [11.0.1] - 2025-11-19

This is a service update that focuses on package dependencies.

## [11.0.0] - 2025-11-11

This is a major release that focuses on adapting to the latest .NET 10 (LTS) release, while also removing support for .NET 8 (LTS).

At the same time we also adapted xUnit v3 as this is the one in active development whereas xUnit v2 is in maintenance mode only.

> [!WARNING]
> Design choices made in xUnit v3 are a bit puzzling to me, especially around something as important as versioning and requirement for executable projects.
> 
> All good practices has been put aside, and for the majority of new packages, major version is baked into the package name itself (e.g., `xunit.v3` instead of just `xunit`).
> 
> Semantic Versioning is all about conveying meaning and intent through version numbers, and this approach introduces unnecessary redundancy and makes long-term maintenance less smooth. Its a mess that could lead others down a slippery slope of wrongdoings due to the popularity of the framework.
>
> Another unfortunate design choice is the requirement of executable projects (i.e., output type `exe` instead of `library`). This has implications when you have optimized your CI to not waste CPU cycles on restoring/building test projects (build once strategy).
>
> Updating to v11.0.0 of this library should not provide issues for you as a consumer, but you should expect consequence changes related to xUnit v3. Because of this, I do recommend familiarizing yourself with the [Migrating Unit Tests from xUnit v2 to v3](https://xunit.net/docs/getting-started/v3/migration) guide.
>
> It's also worth reading the [What's New in xUnit v3](https://xunit.net/docs/getting-started/v3/whats-new) document to get a better understanding of the changes and improvements introduced.
>
> That written, xUnit will continue to remain my preferred test framework for .NET, I just needed to vent my frustration and warn you about the pitfalls of xUnit v3.

### Changed

- Test class in the Codebelt.Extensions.Xunit namespace to use ValueTask for InitializeAsync instead of Task (xUnit v3 consequence change)
- HostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace to use ValueTask for InitializeAsync instead of Task (xUnit v3 consequence change)

## [10.0.7] - 2025-10-17

This is a service update that focuses on package dependencies.

## [10.0.6] - 2025-09-14

This is a service update that focuses on package dependencies.

## [10.0.5] - 2025-08-16

This is a service update that focuses on package dependencies.

## [10.0.4] - 2025-07-10

This is a service update that focuses on package dependencies.

## [10.0.3] - 2025-06-15

This is a service update that focuses on package dependencies and minor improvements.

### Changed

- AddXunitTestLogging method on the ServiceCollectionExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace to use same logic as the overload equivalent and be forgiving when ITestOutputHelper is null (before an InvalidOperationException was thrown)


## [10.0.2] - 2025-06-01

This is a minor update that addresses some non-critical issues and improves the overall developer experience of the package.

### Changed

- HasValidState method on the GenericHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace to exclude check of Host property
- AddXunitTestLogging method on the ServiceCollectionExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace to be forgiving when no active test is present (relates to https://github.com/xunit/xunit/issues/2377)
  - You should use the ITestOutputHelperAccessor capability provided by AddXunitTestLoggingOutputHelperAccessor instead to set the ITestOutputHelper instance in your tests (e.g., in your constructor call `fixture.Host.Services.GetRequiredService<ITestOutputHelperAccessor>().TestOutput = output;`)

### Fixed

- ManagedHostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace to exit ConfigureHost method if the fixture is not in a valid state (e.g., configuration delegate properties are null)
- ManagedWebHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace to exit ConfigureHost method if the fixture is not in a valid state (e.g., configuration delegate properties are null)

## [10.0.1] - 2025-05-24

This is a service update that focuses on package dependencies.

## [10.0.0] - 2025-04-12

This major release introduces support for unit testing Minimal APIs and includes numerous breaking changes with valuable learnings from previous 9.1.x releases. These changes aim to ensure greater consistency across the `Codebelt.Extensions.Xunit.Hosting` and `Codebelt.Extensions.Xunit.Hosting.AspNetCore` namespaces.

### Added

- HostTest class in the Codebelt.Extensions.Xunit.Hosting namespace that represents the non-generic base class from where its generic equivalent should derive (e.g., MinimalHostTest{T}, HostTest{T}, etc.)
- IGenericHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace that provides a set of members for configuring the host
- ManagedHostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace that provides a default implementation of the IGenericHostFixture interface (replaces the legacy HostFixture class)
- GenericHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of one extension method for the IGenericHostFixture interface: HasValidState
- IMinimalHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace that provides a set of members for configuring the host (minimal style)
- ManagedMinimalHostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace that provides a default implementation of the IMinimalHostFixture interface
- MinimalHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace that consist of one extension method for the IMinimalHostFixture interface: HasValidState
- MinimalHostTest class in the Codebelt.Extensions.Xunit.Hosting namespace that represents the non-generic base class from where its generic equivalent should derive (e.g., MinimalWebHostTest, {T}, MinimalHostTest{T}, etc.)
- MinimalHostTestFactory class in the Codebelt.Extensions.Xunit.Hosting namespace that provides a set of static methods for IHost unit testing (minimal style)
- ServiceCollectionExtensions class in the Codebelt.Extensions.Xunit.Hosting namespace received one new extension method for the IServiceCollection interface: An overload of AddXunitTestLogging that does not rely on the ITestOutputHelper interface
  - This was done to help mitigate those scenarios where the [current design of xUnit v2 and ITestOutputHelper can cause deadlocks](https://github.com/xunit/xunit/discussions/2994), e.g. you can access the xUnit logger but nothing is logged to the test output
- WebHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that consist of one extension method for the IWebHostFixture interface: HasValidState
- HostBuilderApplicationExtensions class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that consist of one extension method for the IHostApplicationBuilder interface: ToHostBuilder
- IWebMinimalHostFixture interface in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that provides a way to use Microsoft Dependency Injection in unit tests (minimal style)
- ManagedWebMinimalHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that provides a default implementation of the IWebMinimalHostFixture interface
- WebMinimalHostFixtureExtensions class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that consist of one extension method for the IWebMinimalHostFixture interface: HasValidState
- MinimalWebHostTest{T} class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that represents a base class from which all implementations of unit testing, that uses Microsoft Dependency Injection and depends on ASP.NET Core (minimal style), should derive
- MinimalWebHostTestFactory class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that provides a set of static methods for ASP.NET Core (minimal style) unit testing
- SelfManagedHostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace that represents a self-managed implementation of the ManagedHostFixture class (i.e., the host is not started automatically)
- SelfManagedMinimalHostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace that represents a self-managed implementation of the ManagedMinimalHostFixture class (i.e., the host is not started automatically)
- SelfManagedWebHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that provides a self-managed implementation of the ManagedWebHostFixture class (i.e., the host is not started automatically)
- SelfManagedWebMinimalHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace that provides a self-managed implementation of the ManagedWebMinimalHostFixture class (i.e., the host is not started automatically)

### Changed

- Test class in the Codebelt.Extensions.Xunit namespace to report unhandled exceptions in the test output using the injected ITestOutputHelper interface
- IHostingEnvironmentTest in the Codebelt.Extensions.Xunit.Hosting namespace was renamed to IEnvironmentTest (breaking change)
- GenericHostTestFactory in the Codebelt.Extensions.Xunit.Hosting namespace was renamed to HostTestFactory (breaking change)
- IGenericHostTest in the Codebelt.Extensions.Xunit.Hosting namespace was renamed to IHostTest (breaking change)
- HostFixture class in the Codebelt.Extensions.Xunit.Hosting namespace was changed to an abstract class from which all other host fixture classes derive from (e.g., ManagedWebHostFixture, ManagedHostFixture, etc.)
- IHostFixture interface in the Codebelt.Extensions.Xunit.Hosting namespace was changed to be more confined in scope (e.g., less interface inheritance and ultimately fewer members)
- AspNetCoreHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace was renamed to ManagedWebHostFixture (breaking change)
- AspNetCoreHostTest class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace was renamed to WebHostTest (breaking change)
- BlockingAspNetCoreHostFixture class in the Codebelt.Extensions.Xunit.Hosting.AspNetCore namespace was renamed to BlockingManagedWebHostFixture (breaking change)
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




[Unreleased]: https://github.com/codebeltnet/xunit/compare/v11.2.0...HEAD
[11.2.0]: https://github.com/codebeltnet/xunit/compare/v11.1.2...v11.2.0
[11.1.2]: https://github.com/codebeltnet/xunit/compare/v11.1.1...v11.1.2
[11.1.1]: https://github.com/codebeltnet/xunit/compare/v11.1.0...v11.1.1
[11.1.0]: https://github.com/codebeltnet/xunit/compare/v11.0.10...v11.1.0
[11.0.10]: https://github.com/codebeltnet/xunit/compare/v11.0.9...v11.0.10
[11.0.9]: https://github.com/codebeltnet/xunit/compare/v11.0.8...v11.0.9
[11.0.8]: https://github.com/codebeltnet/xunit/compare/v11.0.7...v11.0.8
[11.0.7]: https://github.com/codebeltnet/xunit/compare/v11.0.6...v11.0.7
[11.0.6]: https://github.com/codebeltnet/xunit/compare/v11.0.5...v11.0.6
[11.0.5]: https://github.com/codebeltnet/xunit/compare/v11.0.4...v11.0.5
[11.0.4]: https://github.com/codebeltnet/xunit/compare/v11.0.3...v11.0.4
[11.0.3]: https://github.com/codebeltnet/xunit/compare/v11.0.2...v11.0.3
[11.0.2]: https://github.com/codebeltnet/xunit/compare/v11.0.1...v11.0.2
[11.0.1]: https://github.com/codebeltnet/xunit/compare/v11.0.0...v11.0.1
