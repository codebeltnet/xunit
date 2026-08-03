namespace Codebelt.Extensions.Xunit.Hosting.Internal;

// Adapted from the ASP.NET Core testing infrastructure.
// Licensed to the .NET Foundation under one or more agreements under the MIT license.
internal interface IDeferredHost
{
    void ReleaseEntrypoint();
}
