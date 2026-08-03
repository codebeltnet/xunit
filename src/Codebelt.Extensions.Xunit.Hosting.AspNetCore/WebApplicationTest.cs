using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

/// <summary>
/// Represents a base class from which all implementations of unit testing, that uses Microsoft Dependency Injection and depends on an existing ASP.NET Core application entry point, should derive.
/// </summary>
/// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
/// <typeparam name="T">The type of the object that implements the <see cref="IWebApplicationFixture{TEntryPoint}"/> interface.</typeparam>
/// <seealso cref="HostTest" />
/// <seealso cref="IClassFixture{TFixture}" />
public abstract class WebApplicationTest<TEntryPoint, T> : HostTest, IClassFixture<T> where TEntryPoint : class where T : class, IWebApplicationFixture<TEntryPoint>
{
    private TestServer _server;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebApplicationTest{TEntryPoint,T}"/> class.
    /// </summary>
    /// <param name="hostFixture">An implementation of the <see cref="IWebApplicationFixture{TEntryPoint}"/> interface.</param>
    /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
    /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
    protected WebApplicationTest(T hostFixture, ITestOutputHelper output = null, Type callerType = null) : this(false, hostFixture, output, callerType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WebApplicationTest{TEntryPoint,T}"/> class.
    /// </summary>
    /// <param name="skipHostFixtureInitialization">A value indicating whether to skip the host fixture initialization.</param>
    /// <param name="hostFixture">An implementation of the <see cref="IWebApplicationFixture{TEntryPoint}"/> interface.</param>
    /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
    /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="hostFixture"/> is null.
    /// </exception>
    protected WebApplicationTest(bool skipHostFixtureInitialization, T hostFixture, ITestOutputHelper output = null, Type callerType = null) : base(output, callerType)
    {
        ArgumentNullException.ThrowIfNull(hostFixture);
        if (skipHostFixtureInitialization) { return; }
        if (!hostFixture.HasValidState())
        {
            hostFixture.ConfigureCallback = Configure;
            hostFixture.ConfigureWebHostCallback = ConfigureWebHost;
            hostFixture.ConfigureHost(this);
        }
        Host = hostFixture.Host;
        Server = hostFixture.Server;
        Configure(hostFixture.Configuration, hostFixture.Environment);
    }

    /// <summary>
    /// Gets the <see cref="TestServer"/> initialized by the <see cref="IHostFixture.Host"/>.
    /// </summary>
    /// <value>The <see cref="TestServer"/> initialized by the <see cref="IHostFixture.Host"/>.</value>
    /// <remarks>Accessing the server starts an entry-point-owned deferred host when necessary.</remarks>
    public TestServer Server
    {
        get
        {
            _ = Host;
            return _server;
        }
        protected set => _server = value;
    }

    /// <summary>
    /// Provides a way to override the <see cref="IWebHostBuilder"/> defaults before the application is built.
    /// </summary>
    /// <param name="builder">The <see cref="IWebHostBuilder"/> used to configure the application.</param>
    protected virtual void ConfigureWebHost(IWebHostBuilder builder)
    {
    }
}
