// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", Justification = "This is a base class implementation of the IDisposable interface tailored to avoid wrong implementations.", Scope = "type", Target = "~T:Codebelt.Extensions.Xunit.Test")]
[assembly: SuppressMessage("Major Code Smell", "S3971:\"GC.SuppressFinalize\" should not be called", Justification = "False-Positive due to IAsyncDisposable living side-by-side with IDisposable.", Scope = "member", Target = "~M:Codebelt.Extensions.Xunit.Test.DisposeAsync~System.Threading.Tasks.ValueTask")]
[assembly: SuppressMessage("Maintainability", "CA1510:Use ArgumentNullException throw helper", Justification = "Wont fix. Multi-TFMs. Only supported with >= .NET 7.", Scope = "member", Target = "~M:Codebelt.Extensions.Xunit.DelimitedString.Create``1(System.Collections.Generic.IEnumerable{``0},System.Action{Codebelt.Extensions.Xunit.DelimitedStringOptions{``0}})~System.String")]
[assembly: SuppressMessage("Maintainability", "CA1510:Use ArgumentNullException throw helper", Justification = "Wont fix. Multi-TFMs. Only supported with >= .NET 7.", Scope = "member", Target = "~M:Codebelt.Extensions.Xunit.TestOutputHelperExtensions.WriteLines(Xunit.Abstractions.ITestOutputHelper,System.Object[])")]
[assembly: SuppressMessage("Maintainability", "CA1510:Use ArgumentNullException throw helper", Justification = "Wont fix. Multi-TFMs. Only supported with >= .NET 7.", Scope = "member", Target = "~M:Codebelt.Extensions.Xunit.TestOutputHelperExtensions.WriteLines``1(Xunit.Abstractions.ITestOutputHelper,System.Collections.Generic.IEnumerable{``0})")]
