﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Blocker Code Smell", "S3427:Method overloads with default parameter values should not overlap", Justification = "Avoid bumping major version by providing an extra overloaded member as optional argument.", Scope = "member", Target = "~M:Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebHostTestFactory.Create(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{Microsoft.AspNetCore.Builder.IApplicationBuilder},System.Action{Microsoft.Extensions.Hosting.IHostBuilder},Codebelt.Extensions.Xunit.Hosting.AspNetCore.IAspNetCoreHostFixture)~Codebelt.Extensions.Xunit.Hosting.AspNetCore.IWebHostTest")]
[assembly: SuppressMessage("Blocker Code Smell", "S3427:Method overloads with default parameter values should not overlap", Justification = "Avoid bumping major version by providing an extra overloaded member as optional argument.", Scope = "member", Target = "~M:Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebHostTestFactory.CreateWithHostBuilderContext(System.Action{Microsoft.Extensions.Hosting.HostBuilderContext,Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{Microsoft.Extensions.Hosting.HostBuilderContext,Microsoft.AspNetCore.Builder.IApplicationBuilder},System.Action{Microsoft.Extensions.Hosting.IHostBuilder},Codebelt.Extensions.Xunit.Hosting.AspNetCore.IAspNetCoreHostFixture)~Codebelt.Extensions.Xunit.Hosting.AspNetCore.IWebHostTest")]
[assembly: SuppressMessage("Blocker Code Smell", "S3427:Method overloads with default parameter values should not overlap", Justification = "Avoid bumping major version by providing an extra overloaded member as optional argument.", Scope = "member", Target = "~M:Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebHostTestFactory.RunAsync(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{Microsoft.AspNetCore.Builder.IApplicationBuilder},System.Action{Microsoft.Extensions.Hosting.IHostBuilder},System.Func{System.Net.Http.HttpClient,System.Threading.Tasks.Task{System.Net.Http.HttpResponseMessage}},Codebelt.Extensions.Xunit.Hosting.AspNetCore.IAspNetCoreHostFixture)~System.Threading.Tasks.Task{System.Net.Http.HttpResponseMessage}")]
[assembly: SuppressMessage("Blocker Code Smell", "S3427:Method overloads with default parameter values should not overlap", Justification = "Avoid bumping major version by providing an extra overloaded member as optional argument.", Scope = "member", Target = "~M:Codebelt.Extensions.Xunit.Hosting.AspNetCore.WebHostTestFactory.RunWithHostBuilderContextAsync(System.Action{Microsoft.Extensions.Hosting.HostBuilderContext,Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{Microsoft.Extensions.Hosting.HostBuilderContext,Microsoft.AspNetCore.Builder.IApplicationBuilder},System.Action{Microsoft.Extensions.Hosting.IHostBuilder},System.Func{System.Net.Http.HttpClient,System.Threading.Tasks.Task{System.Net.Http.HttpResponseMessage}},Codebelt.Extensions.Xunit.Hosting.AspNetCore.IAspNetCoreHostFixture)~System.Threading.Tasks.Task{System.Net.Http.HttpResponseMessage}")]
