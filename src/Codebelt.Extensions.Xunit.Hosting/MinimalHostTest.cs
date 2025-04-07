using System;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Represents the non-generic base class from where its generic equivalent should derive.
    /// </summary>
    /// <seealso cref="HostTest" />
    public abstract class MinimalHostTest : HostTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinimalHostTest"/> class.
        /// </summary>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        protected MinimalHostTest(ITestOutputHelper output = null, Type callerType = null) : base(output, callerType)
        {
        }

        
        /// <summary>
        /// Provides a way to override the <see cref="IHostApplicationBuilder"/> defaults.
        /// </summary>
        /// <param name="hb">The <see cref="IHostApplicationBuilder"/> that initializes an instance of <see cref="IHost"/>.</param>
        protected virtual void ConfigureHost(IHostApplicationBuilder hb)
        {
        }
    }

    /// <summary>
    /// Represents a base class from which all implementations of unit testing, that uses Microsoft Dependency Injection (minimal style), should derive.
    /// </summary>
    /// <typeparam name="T">The type of the object that implements the <see cref="IGenericHostFixture"/> interface.</typeparam>
    /// <seealso cref="HostTest" />
    /// <seealso cref="IClassFixture{TFixture}" />
    /// <remarks>The class needed to be designed in this rather complex way, as this is the only way that xUnit supports a shared context. The need for shared context is theoretical at best, but it does opt-in for Scoped instances.</remarks>
    public abstract class MinimalHostTest<T> : MinimalHostTest, IClassFixture<T> where T : class, IMinimalHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostTest{T}"/> class.
        /// </summary>
        /// <param name="hostFixture">An implementation of the <see cref="IMinimalHostFixture"/> interface.</param>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hostFixture"/> is null.
        /// </exception>
        protected MinimalHostTest(T hostFixture, ITestOutputHelper output = null, Type callerType = null) : this(false, hostFixture, output, callerType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostTest{T}"/> class.
        /// </summary>
        /// <param name="skipHostFixtureInitialization">A value indicating whether to skip the host fixture initialization.</param>
        /// <param name="hostFixture">An implementation of the <see cref="IGenericHostFixture"/> interface.</param>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hostFixture"/> is null.
        /// </exception>
        protected MinimalHostTest(bool skipHostFixtureInitialization, T hostFixture, ITestOutputHelper output = null, Type callerType = null) : base(output, callerType)
        {
            if (hostFixture == null) { throw new ArgumentNullException(nameof(hostFixture)); }
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
    }
}
