// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S3881:\"IDisposable\" should be implemented correctly", Justification = "This is a base class implementation of the IDisposable interface tailored to avoid wrong implementations.", Scope = "type", Target = "~T:Codebelt.Extensions.Xunit.Test")]
