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
- **Prefer file-scoped namespaces** (`namespace Codebelt.Extensions.Xunit;`) for new files.
- The current majority of the codebase uses **block-scoped namespaces** — do not convert existing files unless explicitly asked.
- When editing an existing file, follow whichever style that file already uses.
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
