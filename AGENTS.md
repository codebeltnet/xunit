# AGENTS.md - Codebelt.Extensions.Xunit

Guide for AI agents working on this .NET xUnit extensions library.

## Project Overview

A .NET library providing extensions for xUnit v3 testing framework. Supports multi-targeting: `net10.0`, `net9.0`, `netstandard2.0` (source) and `net48` (tests on Windows).

## Repository Layout

- Solution: `Codebelt.Extensions.Xunit.slnx` in repo root.
- `src/` — NuGet packages (shipped to nuget.org).
- `test/` — xUnit v3 unit and functional tests.
- `tuning/` — BenchmarkDotNet benchmarks.
- `tooling/` — internal CLI tools.
- `.nuget/<PackageName>/` — per-package `README.md` and `PackageReleaseNotes.txt`.

## Toolchain

- .NET SDK with `LangVersion=latest`.
- Source TFMs: `net10.0;net9.0;netstandard2.0`.
- Test TFMs: `net10.0;net9.0` on Linux; adds `net48` on Windows.
- Benchmark TFMs: `net10.0;net9.0;netstandard2.0`.
- Central package management via `Directory.Packages.props` (`ManagePackageVersionsCentrally=true`).
- CI runs on Linux (ubuntu-24.04) and Windows (windows-2025), both X64 and ARM64.
- TFM compatibility is mandatory: proposals and code changes must work for all source TFMs. Do not assume `net9.0`/`net10.0` APIs exist in `netstandard2.0`; use conditional compilation (`#if NET9_0_OR_GREATER`) or compatible fallbacks where needed.

## Build Commands

```bash
# Build entire solution
dotnet build Codebelt.Extensions.Xunit.slnx

# Build Release configuration
dotnet build Codebelt.Extensions.Xunit.slnx -c Release

# Build with skipped assembly signing (for CI/external contributors)
dotnet build -p:SkipSignAssembly=true

# Restore packages
dotnet restore Codebelt.Extensions.Xunit.slnx

# Pack NuGet packages
dotnet pack -c Release
```

## Lint / Analyzers

- No separate lint step; code style is enforced during build (`EnforceCodeStyleInBuild=true` for source projects).
- Analyzers are **disabled** for test and benchmark projects (`RunAnalyzers=false`, `AnalysisLevel=none`).
- Run `dotnet build -c Release` on source projects to surface style violations.

## Test Commands

```bash
# Run all tests
dotnet test

# Run tests for specific project
dotnet test test/Codebelt.Extensions.Xunit.Tests

# Run single test by fully qualified name
dotnet test --filter "FullyQualifiedName~TestTest"

# Run tests with specific trait
dotnet test --filter "Category=Unit"

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Benchmarks

- Live under `tuning/`; run with `tooling/benchmark-runner`.
- Not unit tests; do not include in test runs.

## Cursor / Copilot Rules

- No Cursor rules (`.cursor/rules/` and `.cursorrules` are absent).
- Copilot rules live in `.github/copilot-instructions.md` — **must follow**.

## Code Style and Conventions

### General Principles
- Follow Framework Design Guidelines and Microsoft Engineering Guidelines.
- Adhere to SOLID, DRY, separation of concerns.
- Apply the boy scout rule; do not duplicate code.

### Formatting
- 4 spaces for `.cs` files; 2 spaces for `.xml` (`.editorconfig`).
- Keep existing style in files; many modern analyzers are explicitly disabled.

### Namespace Style
- **Always use file-scoped namespaces** (`namespace Codebelt.Extensions.Xunit;`) — the entire codebase has been refactored to file-scoped namespaces.
- **Never use block-scoped namespaces** for new or edited files.
- **Never use top-level statements.** Always use explicit class declarations with a proper namespace.

### Disabled Analyzers (key rules — do NOT introduce these patterns)

| Rule | What it forces | Why disabled |
|------|---------------|--------------|
| IDE0066 | switch expressions | style consistency |
| IDE0063 | using declarations | style consistency |
| IDE0290 | primary constructors | style consistency |
| IDE0022 | expression-bodied methods | style consistency |
| IDE0300/0301/0028/0305 | collection expressions | netstandard2.0 compat |
| CA1846/1847/1865-1867 | Span/char overloads | netstandard2.0 compat |
| IDE0330 | `System.Threading.Lock` | requires net9.0+ |
| Performance category | various | netstandard2.0 compat |

### Namespaces
- **CRITICAL**: Test namespaces MUST match the System Under Test (SUT) exactly
- Do NOT append `.Tests` or `.Benchmarks` to namespaces
- Example: SUT `Codebelt.Extensions.Xunit` → Tests `Codebelt.Extensions.Xunit` (not `Codebelt.Extensions.Xunit.Tests`)
- Override `RootNamespace` in `.csproj` to match SUT namespace

### Test Classes
- Always inherit from `Test` base class from `Codebelt.Extensions.Xunit`
- Constructor must accept `ITestOutputHelper output` and pass to base
- Class names end with `Test` (e.g., `DateSpanTest`)

```csharp
using Codebelt.Extensions.Xunit;
using Xunit;

namespace Codebelt.Extensions.Xunit  // Same as SUT
{
    public class YourTestClass : Test
    {
        public YourTestClass(ITestOutputHelper output) : base(output) { }
    }
}
```

### Imports
- Use `using Xunit;` - NOT `Xunit.Abstractions` (xUnit v3 removed this namespace)
- Use `using Xunit.v3;` when needed for xUnit v3 specific types
- Place System.* usings first, then third-party, then project
- Keep `using` directives explicit and minimal.
- Follow existing ordering; do not auto-reorder.

### Naming Conventions
- Test methods: Use descriptive names with `Should` prefix
  - Pattern: `Should{ExpectedResult}_When{Condition}`
  - Example: `ShouldReturnTrue_WhenConditionIsMet`
- Use `[Fact]` for standard tests, `[Theory]` for parameterized tests
- Benchmark classes: End with `Benchmark`

### Types and `var`
- Do not blindly enforce `var`; use explicit types when it improves clarity.
- IDE0008 (use explicit type) is disabled — either form is acceptable.

### Error Handling
- Use guard clauses with `ArgumentNullException`, `ArgumentOutOfRangeException`
- **Always use `Validator` pattern** (e.g., `Validator.ThrowIfNull(param)`)
- Pattern: `if (param == null) { throw new ArgumentNullException(nameof(param)); }` only when Validator is not available
- Use `ArgumentOutOfRangeException` for validation with actual/expected values
- Prefer deterministic, testable error paths; never swallow exceptions.

### XML Documentation
- Document all public/protected APIs with XML comments
- Use `<see cref="TypeName"/>` for type references
- Include `<exception>` tags for thrown exceptions
- Follow existing documentation style (see Hash.cs example)

## Testing Guidelines

### Test Doubles
- Preferred: dummies, fakes, stubs, spies
- Mocks allowed: Moq library for special circumstances only
- Never mock `IMarshaller`; use `JsonMarshaller` instance instead

### Internal Members
- Do NOT use `InternalsVisibleTo`
- Test internal logic via public APIs that consume them
- Use Public Facade Testing pattern

### Async Tests
- Override `InitializeAsync()` for async setup
- Use `ValueTask` for async operations (not `Task`)
- Implement `IAsyncLifetime` when needed via `Test` base class

## Benchmark Guidelines

- Place in `tuning/` folder or `*.Benchmarks` projects
- Namespaces follow same rule as tests (no `.Benchmarks` suffix)
- Use `[MemoryDiagnoser]` and `[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]` attributes.
- Use `[GlobalSetup]` for expensive prep; keep measured methods focused.
- Use `[Params]` for multiple input sizes; use deterministic data; avoid external systems.
- Mark one method `Baseline = true`; use descriptive `Description` values.

## Package Management

- Uses Central Package Management (`Directory.Packages.props`)
- Do not add version numbers in individual `.csproj` files
- Test frameworks are centrally managed

## CI/Build Notes

- Assembly signing uses `xunit.snk` (skip for external builds)
- MinVer handles versioning based on Git tags
- Code coverage via coverlet
- SonarCloud and CodeQL analysis enabled

## Release Notes

- Per-package notes in `.nuget/<PackageName>/PackageReleaseNotes.txt`.
- Keep updated for public API changes.

## Official Documentation

- Public API conventions belong in `.docfx/api/namespaces/` and should be treated as the official documentation source for library behavior and naming vocabulary.
- When adding or renaming public APIs, update the relevant namespace page in `.docfx/api/namespaces/` if the change introduces or clarifies a convention.
- Keep internal reasoning, exploratory notes, and agent discussion out of DocFX pages; summarize only stable public guidance.

## Commit Style (Gitmoji)

This repo uses **gitmoji** commit messages — do **not** use Conventional Commits (`feat:`, `fix:`, etc.).

Format: `<emoji> <subject>`

**Always use the actual Unicode emoji character**, not the GitHub shortcode (e.g., use `✨` not `:sparkles:`).

Example: `✨ Add Test.Match wildcard overload`

### Common Gitmojis

| Emoji | Use for |
|-------|---------|
| ✨ | New feature |
| 🐛 | Bug fix |
| ♻️ | Refactoring |
| ✅ | Adding / updating unit test / functional test |
| 📝 | Documentation |
| ⚡ | Performance improvement |
| 🎨 | Code style / formatting |
| 🔥 | Removing code or files |
| 🚧 | Work in progress |
| 📦 | Package / dependency update |
| 🔧 | Configuration / tooling |
| 🚚 | Moving / renaming files |
| 💥 | Breaking change |
| 🩹 | Non-critical fix |

### Rules

1. **One emoji per commit** — each commit has exactly one primary gitmoji.
2. **Be specific** — choose the most appropriate emoji, not a generic one.
3. **Consistent scope** — use consistent scope names across commits.
4. **Clear messages** — the subject line should be understandable without a body.
5. **Atomic commits** — each commit should be independently buildable and testable.

## Agent Workflow

1. Identify the correct project area (`src/`, `test/`, `tuning/`, `tooling/`).
2. Follow namespace and naming rules **before** writing any code.
3. Before potentially refactoring any code, verify the code in question is well tested; if coverage is missing, add or update tests first to reduce regression risk.
4. Build the affected source project to check for style violations.
5. Run targeted tests when changing logic.
6. Keep changes minimal and consistent with existing local style.

## Copilot Instructions

See `.github/copilot-instructions.md` for detailed guidelines on:
- Writing unit tests
- Writing performance tests (BenchmarkDotNet)
- Writing XML documentation

<!-- dotnet-docfx-digest:start -->
## DocFX Documentation Maintenance

When changing public .NET APIs, keep the DocFX documentation current in the same change set.

Documentation updates must cover public API only. Do not document private or internal types or members. Do not create namespace overview pages for namespaces that contain no public API.

Public non-abstraction types — including enums, structs, records, plain classes, and static extension containers — are valid documentation targets. Generic public types and generic extension methods are valid documentation targets too. Do not exclude a type solely because it is generic or because reflection reports it as abstract and sealed (that is the IL pattern for a static class).

For public non-abstraction types, include at least one realistic, copy/paste-ready usage example on the generated type page/overwrite section for that type UID. For example, a public `Class1` requires an example on the `Class1` API page, not only on the namespace page. Prefer deriving examples from existing unit, functional, or integration tests, but convert test code into real-life consumer-oriented usage.

Missing type examples must be added through per-type DocFX overwrite files under `.docfx/api/types/{TypeUid}.md` in Codebelt repositories. Namespace overview text and `Extension Members` tables are not substitutes for type-page examples.

Public extension methods must have examples too. Listing an extension method in an `Extension Members` table is required, but it is not enough.

All added or changed code samples must be deterministic and verified to compile. Do not add pseudo-code, ellipses, hidden test helpers, or examples that rely on unverified behavior.

Compilation is necessary but not sufficient. Do not present runtime implementation names such as `services.GetType().Name` or `host.GetType().FullName` as the example outcome. Show application behavior, configured state, a resolved domain service, an HTTP response, or another result that explains why a caller uses the API. Application-entry-point examples must not declare an empty local `Program` type merely to compile; show a real entry point or clearly identify the referenced application project.

Every namespace containing public API must have a DocFX namespace overview page named after the namespace, such as `X.Y.Z.md`, under `.docfx/api/namespaces/`, using DocFX overwrite front matter with the namespace `uid`.

Namespace pages must identify key entry points from release notes, package documentation, public factories/builders, and strong functional tests, then help readers choose among adjacent workflows. When the package complements a well-known upstream API, compare concrete acquisition, customization, lifecycle, and sharing tradeoffs from current official guidance; do not claim drop-in replacement compatibility without evidence.

Namespaces exposing public extension methods must document those extension members at namespace level. The namespace page must include an `Extension Members` table listing the extended type, the extension marker, and the public extension methods. Extension members are rendered under the heading `Extension Members`.

Both namespace overwrite files and type overwrite files are required deliverables in the same run. Generating only namespace pages or only type pages is incomplete.

`docfx.json` must keep namespace and type overwrite files in separate subdirectories. `build.overwrite` must include both `api/namespaces/**/*.md` (for namespace pages) and `api/types/**/*.md` (for type pages). `build.content` must exclude both `api/namespaces/**` and `api/types/**` to prevent overwrite Markdown from being treated as conceptual content. Do not use `api/**/*.md` under `build.overwrite` or `build.content`.

Availability must be documented by referencing the appropriate include file when one exists, or by adding explicit availability text when no suitable include exists. Availability must reflect the actual target frameworks, conditional compilation, and project configuration.

For conditionally compiled APIs, choose the executable test framework from the asset that contains the API. Inspect the preprocessor condition, project TFMs, package `lib/` assets, and resolved consumer asset before changing a sample. For APIs under `NETSTANDARD2_0` or `NETSTANDARD2_0_OR_GREATER`, when modern `lib/netX.0/` assets also exist, use `net48` (or another supported .NET Framework target from `net462` onward) so the consumer selects `lib/netstandard2.0/`. Never use `netstandard*` as an executable target, and never use a modern `netX.0` target when it selects an asset where the API is absent. For other TFM guards, select a runnable consumer TFM that resolves to the containing asset and confirm that selection from restore or build evidence.

Preserve manual documentation edits. Prefer additive changes, but correct stale or contradictory information so documentation remains accurate.

Preserve working Markdown links, `Related:` references, and historical URL citations during prose rewrites. Remove or replace a URL only after directly verifying that the current destination returns HTTP 404. Timeouts, 403s, rate limits, DNS failures, and other lookup problems are not removal evidence.

Interim scratch artifacts do not belong in the repository working tree. Store assessment queues, project manifests, review reports, captured validator output, progress notes, and one-off helper scripts in temp or session storage instead. New working-tree files are only legitimate when they are the managed `AGENTS.md` block, the active `docfx.json`, the deterministic `skip-compile-allowlist.json` waiver file when one is truly required, or DocFX-authored namespace/type Markdown that maps to a real public namespace or type. Everything else is blocking cleanup work, not a documentation deliverable. The validator auto-detects generic-arity type families (such as `MutableTuple`1`..`MutableTuple`N`) and skips redundant sibling examples from the public API surface alone, so no family-skip manifest is ever written into the repository.

Skip markers are waivers, not fixes. A skip marker only suppresses compilation when it both existed before the current run and matches an entry in `.docfx/skip-compile-allowlist.json`. Each allowlist entry must include `diagnosticCode`, `filePath`, `uid` or `symbol`, `reason`, `approval`, and `lifetime` (`temporary` or `permanent`). Newly introduced or unallowlisted skip markers remain fail-level diagnostics and do not permit a completion claim.

Do not emit a final report, audit result, completion summary, or handoff while `summary.canClaimCompletion` is false, `summary.remainingWorkItems` is greater than zero, `summary.remainingGates` is non-empty, `summary.fullVerificationRan` is false, fail-level diagnostics remain, `summary.newlyIntroducedSkipMarkers` is non-zero, or `summary.interimArtifacts` is non-zero. Large queues, many changed files, repetitive next steps, long runtimes, context pressure, session length, task size, or a "stable queue" are not valid stop reasons; the next action must be another remediation batch, a validator rerun, a validator/tooling fix, or a true blocker with exact evidence.

Context pressure is not a completion condition. If the session feels constrained while work remains, continue with a smaller deterministic batch, regenerate deterministic queue state such as `--assessment-queue`, `--project-manifest`, or the active dry-run manifest/review pair, or report a true tooling failure with the exact command, exit code, and output. When naming a queue-state regeneration command, resolve it to a concrete temp/session path instead of leaving `<temp-path>` as a placeholder. Do not stop with phrases like "given context constraints", "best done in a follow-up", "remaining work requires authoring", "this is a massive task", or "I will provide a focused summary". A context-sized handoff while work remains is `FAIL_CONTEXT_HANDOFF_WITH_REMAINING_WORK`; the remediation is to continue with a smaller deterministic batch.

Before completing documentation work, run the relevant verification commands, normally:

```bash
dotnet build
dotnet test
dotnet run --file <resolved-skill-dir>/scripts/docfx.cs -- --repo-root . --build-api-model --validate-samples --verify-docfx-build
```

Codebelt repositories are normally strong-name signed with a `.snk` file in the repository root on the main author's codespace. Preserve and copy that root `.snk` file when building a temporary copy. If the repository or temp copy has no root `.snk`, run build and test verification with `-p:SkipSignAssembly=true`, for example `dotnet build -p:SkipSignAssembly=true` and `dotnet test -p:SkipSignAssembly=true`.

The final DocFX verification must run outside the working tree when possible. The `--verify-docfx-build` option copies the repository to a temp workspace, runs DocFX against the resolved `docfx.json` there, and removes the temp workspace afterward so generated API YAML, manifest files, and site output do not flood git status. Do not call the work complete until the final JSON reports `summary.fullVerificationRan: true`, `summary.canClaimCompletion: true`, `summary.remainingWorkItems: 0`, an empty `summary.remainingGates`, an empty `summary.remainingDiagnosticsByCode`, `summary.newlyIntroducedSkipMarkers: 0`, and `summary.interimArtifacts: 0`.

If a command cannot be run, report the exact limitation or failure instead of claiming the documentation was verified.
<!-- dotnet-docfx-digest:end -->
