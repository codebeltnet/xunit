using System;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting;

/// <summary>
/// Provides a way to use Microsoft Dependency Injection in tests that bootstrap an existing .NET application entry point.
/// </summary>
/// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
/// <seealso cref="IHostFixture" />
public interface IApplicationFixture<TEntryPoint> : IHostFixture where TEntryPoint : class
{
    /// <summary>
    /// Gets or sets the delegate that provides a way to override the <see cref="IHostBuilder"/> before the application is built.
    /// </summary>
    /// <value>The delegate that provides a way to override the <see cref="IHostBuilder"/>.</value>
    Action<IHostBuilder> ConfigureHostCallback { get; set; }

    /// <summary>
    /// Creates and configures the <see cref="IHostFixture.Host"/> of this <see cref="IApplicationFixture{TEntryPoint}"/>.
    /// </summary>
    /// <param name="hostTest">The object that inherits from <see cref="ApplicationTest{TEntryPoint,T}"/>.</param>
    /// <remarks><paramref name="hostTest"/> was added to support those cases where the caller is required in the host configuration.</remarks>
    void ConfigureHost(Test hostTest);
}
