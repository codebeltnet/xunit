using System;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting;

/// <summary>
/// Represents a base class from which all implementations of unit testing, that uses Microsoft Dependency Injection and depends on an existing .NET application entry point, should derive.
/// </summary>
/// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
/// <typeparam name="T">The type of the object that implements the <see cref="IApplicationFixture{TEntryPoint}"/> interface.</typeparam>
/// <seealso cref="HostTest" />
/// <seealso cref="IClassFixture{TFixture}" />
public abstract class ApplicationTest<TEntryPoint, T> : HostTest, IClassFixture<T> where TEntryPoint : class where T : class, IApplicationFixture<TEntryPoint>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationTest{TEntryPoint,T}"/> class.
    /// </summary>
    /// <param name="hostFixture">An implementation of the <see cref="IApplicationFixture{TEntryPoint}"/> interface.</param>
    /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
    /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
    protected ApplicationTest(T hostFixture, ITestOutputHelper output = null, Type callerType = null) : this(false, hostFixture, output, callerType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationTest{TEntryPoint,T}"/> class.
    /// </summary>
    /// <param name="skipHostFixtureInitialization">A value indicating whether to skip the host fixture initialization.</param>
    /// <param name="hostFixture">An implementation of the <see cref="IApplicationFixture{TEntryPoint}"/> interface.</param>
    /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
    /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="hostFixture"/> is null.
    /// </exception>
    protected ApplicationTest(bool skipHostFixtureInitialization, T hostFixture, ITestOutputHelper output = null, Type callerType = null) : base(output, callerType)
    {
#if NETSTANDARD2_0
        if (hostFixture == null) { throw new ArgumentNullException(nameof(hostFixture)); }
#else
        ArgumentNullException.ThrowIfNull(hostFixture);
#endif
        if (skipHostFixtureInitialization) { return; }
        if (!hostFixture.HasValidState())
        {
            hostFixture.ConfigureCallback = Configure;
            hostFixture.ConfigureHostCallback = ConfigureHost;
            hostFixture.ConfigureHost(this);
        }
        Host = hostFixture.Host;
        Configure(hostFixture.Configuration, hostFixture.Environment);
    }

    /// <summary>
    /// Provides a way to override the <see cref="IHostBuilder"/> defaults before the application is built.
    /// </summary>
    /// <param name="builder">The <see cref="IHostBuilder"/> used to configure the application.</param>
    protected virtual void ConfigureHost(IHostBuilder builder)
    {
    }
}
