using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

/// <summary>
/// Provides a way to use Microsoft Dependency Injection in tests that bootstrap an existing ASP.NET Core application entry point.
/// </summary>
/// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
/// <seealso cref="IHostFixture" />
public interface IWebApplicationFixture<TEntryPoint> : IHostFixture where TEntryPoint : class
{
    /// <summary>
    /// Gets or sets the delegate that provides a way to override the <see cref="IWebHostBuilder"/> before the application is built.
    /// </summary>
    /// <value>The delegate that provides a way to override the <see cref="IWebHostBuilder"/>.</value>
    Action<IWebHostBuilder> ConfigureWebHostCallback { get; set; }

    /// <summary>
    /// Gets the <see cref="TestServer"/> initialized by this instance.
    /// </summary>
    /// <value>The <see cref="TestServer"/> initialized by this instance.</value>
    TestServer Server { get; }

    /// <summary>
    /// Creates and configures the <see cref="IHostFixture.Host"/> of this <see cref="IWebApplicationFixture{TEntryPoint}"/>.
    /// </summary>
    /// <param name="hostTest">The object that inherits from <see cref="WebApplicationTest{TEntryPoint,T}"/>.</param>
    /// <remarks><paramref name="hostTest"/> was added to support those cases where the caller is required in the host configuration.</remarks>
    void ConfigureHost(Test hostTest);
}
