---
uid: Codebelt.Extensions.Xunit
summary: *content
---
Use `Test` as the base class for xUnit tests that need a shared output helper, lifecycle support, or wildcard assertions. Use `InMemoryTestStore<T>` when a test needs a small in-process collection that can be queried without external storage. `TestOutputHelperExtensions` writes several values as separate output lines.

When methods in one test class must run in sequence, apply `[TestMethodOrderer(typeof(PriorityTestMethodOrderer))]` to the class and `[Priority(n)]` to its methods. Lower numbers run first, equal priorities are ordered by method name, and methods without a priority run last. xUnit still runs separate test classes in parallel unless the test assembly disables that behavior.

[!INCLUDE [availability-default](../../includes/availability-default.md)]

Complements: [xUnit: Capturing Output](https://xunit.net/docs/capturing-output) 🔗

### Extension Members

|Type|Ext|Methods|
|--:|:-:|---|
|ITestOutputHelper|⬇️|`WriteLines`, `WriteLines<T>`|
|String|⬇️|`ReplaceLineEndings` (TFM netstandard2.0)|
