using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

/// <summary>
/// Provides an entrypoint-owned implementation of the <see cref="IWebApplicationFixture{TEntryPoint}"/> interface.
/// </summary>
/// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
/// <seealso cref="HostFixture" />
/// <seealso cref="IWebApplicationFixture{TEntryPoint}" />
/// <remarks>
/// The ASP.NET Core application's <c>Main</c> method owns host startup. The fixture captures the host after it has been built without starting it during fixture setup; the test host starts the deferred host when it is consumed.
/// </remarks>
public class ManagedWebApplicationFixture<TEntryPoint> : HostFixture, IWebApplicationFixture<TEntryPoint> where TEntryPoint : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManagedWebApplicationFixture{TEntryPoint}"/> class.
    /// </summary>
    public ManagedWebApplicationFixture()
    {
    }

    /// <summary>
    /// Creates and configures the <see cref="IHost"/> of this instance.
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

        Host = CreateEntrypointOwnedHost<TEntryPoint>(hostBuilder => hostBuilder.ConfigureWebHost(webHostBuilder =>
        {
            webHostBuilder.UseTestServer(o => o.PreserveExecutionContext = true);
            ConfigureWebHostCallback?.Invoke(webHostBuilder);
        }));
        try
        {
            Server = Host.GetTestServer();
            Configuration = Host.Services.GetRequiredService<IConfiguration>();
            Environment = Host.Services.GetRequiredService<IHostEnvironment>();

            ConfigureCallback(Configuration, Environment);
        }
        finally
        {
            ReleaseEntrypoint(Host);
        }
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
