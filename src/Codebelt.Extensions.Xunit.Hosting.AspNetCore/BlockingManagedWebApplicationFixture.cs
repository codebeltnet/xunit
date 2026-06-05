using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

/// <summary>
/// Provides a blocking managed implementation of the <see cref="IWebApplicationFixture{TEntryPoint}"/> interface.
/// </summary>
/// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
/// <seealso cref="HostFixture" />
/// <seealso cref="IWebApplicationFixture{TEntryPoint}" />
/// <remarks>
/// Unlike the base managed web host fixtures, this fixture starts the resolved application host synchronously.
/// ASP.NET Core application entry point testing must expose a started <see cref="TestServer"/> after fixture initialization.
/// There is no separate non-blocking managed web application fixture because this application-entry-point API is new and unreleased.
/// </remarks>
public class BlockingManagedWebApplicationFixture<TEntryPoint> : HostFixture, IWebApplicationFixture<TEntryPoint> where TEntryPoint : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockingManagedWebApplicationFixture{TEntryPoint}"/> class.
    /// </summary>
    public BlockingManagedWebApplicationFixture()
    {
        AsyncHostRunnerCallback = (host, _) =>
        {
            host.Start();
            return Task.CompletedTask;
        };
    }

    /// <summary>
    /// Creates and configures the <see cref="IHost" /> of this instance.
    /// </summary>
    /// <param name="hostTest">The object that inherits from <see cref="WebApplicationTest{TEntryPoint,T}"/>.</param>
    /// <remarks><paramref name="hostTest"/> was added to support those cases where the caller is required in the host configuration.</remarks>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="hostTest"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="hostTest"/> is not assignable from <see cref="WebApplicationTest{TEntryPoint,T}"/>.
    /// </exception>
    public virtual void ConfigureHost(Test hostTest)
    {
        ArgumentNullException.ThrowIfNull(hostTest);
        if (!HasTypes(hostTest.GetType(), typeof(WebApplicationTest<,>))) { throw new ArgumentOutOfRangeException(nameof(hostTest), typeof(WebApplicationTest<,>), $"{nameof(hostTest)} is not assignable from WebApplicationTest<TEntryPoint, T>."); }
        if (this.HasValidState()) { return; }

        Host = WebApplicationHostFactory.Create<TEntryPoint>(ConfigureWebHostCallback);
        Server = Host.GetTestServer();
        Configuration = Host.Services.GetRequiredService<IConfiguration>();
        Environment = Host.Services.GetRequiredService<IHostEnvironment>();

        ConfigureCallback(Configuration, Environment);

        AsyncHostRunnerCallback(Host, CancellationToken.None);
    }

    /// <summary>
    /// Gets or sets the delegate that provides a way to override the <see cref="IWebHostBuilder"/> before the application is built.
    /// </summary>
    /// <value>The delegate that provides a way to override the <see cref="IWebHostBuilder"/>.</value>
    public Action<IWebHostBuilder> ConfigureWebHostCallback { get; set; }

    /// <summary>
    /// Gets the <see cref="TestServer"/> initialized by this instance.
    /// </summary>
    /// <value>The <see cref="TestServer"/> initialized by this instance.</value>
    public TestServer Server { get; protected set; }
}
