﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", Justification = "This is an implementation of the IDisposable interface tailored to avoid wrong implementations.", Scope = "type", Target = "~T:Codebelt.Extensions.Xunit.Hosting.HostFixture")]
[assembly: SuppressMessage("Major Code Smell", "S3971:\"GC.SuppressFinalize\" should not be called", Justification = "False-Positive due to IAsyncDisposable living side-by-side with IDisposable.", Scope = "member", Target = "~M:Codebelt.Extensions.Xunit.Hosting.HostFixture.DisposeAsync~System.Threading.Tasks.ValueTask")]
