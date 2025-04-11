using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Represents a base class from which all implementations of unit testing, that uses Microsoft Dependency Injection and depends on ASP.NET Core (minimal style), should derive.
    /// </summary>
    /// <typeparam name="T">The type of the object that implements the <see cref="IWebMinimalHostFixture"/> interface.</typeparam>
    /// <seealso cref="MinimalHostTest" />
    /// <seealso cref="IWebHostTest"/>
    /// <seealso cref="IClassFixture{TFixture}" />
    /// <remarks>The class needed to be designed in this rather complex way, as this is the only way that xUnit supports a shared context. The need for shared context is theoretical at best, but it does opt-in for Scoped instances.</remarks>
    public abstract class MinimalWebHostTest<T> : MinimalHostTest, IWebHostTest, IClassFixture<T> where T : class, IWebMinimalHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinimalWebHostTest{T}"/> class.
        /// </summary>
        /// <param name="hostFixture">An implementation of the <see cref="IWebMinimalHostFixture"/> interface.</param>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        protected MinimalWebHostTest(T hostFixture, ITestOutputHelper output = null, Type callerType = null) : this(false, hostFixture, output, callerType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimalWebHostTest{T}"/> class.
        /// </summary>
        /// <param name="skipHostFixtureInitialization">A value indicating whether to skip the host fixture initialization.</param>
        /// <param name="hostFixture">An implementation of the <see cref="IWebMinimalHostFixture"/> interface.</param>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hostFixture"/> is null.
        /// </exception>
        protected MinimalWebHostTest(bool skipHostFixtureInitialization, T hostFixture, ITestOutputHelper output = null, Type callerType = null) : base(output, callerType)
        {
            if (hostFixture == null) { throw new ArgumentNullException(nameof(hostFixture)); }
            if (skipHostFixtureInitialization) { return; }
            if (!hostFixture.HasValidState())
            {
                hostFixture.ConfigureCallback = Configure;
                hostFixture.ConfigureHostCallback = ConfigureHost;
                hostFixture.ConfigureApplicationCallback = ConfigureApplication;
                hostFixture.ConfigureHost(this);
            }
            Host = hostFixture.Host;
            Application = hostFixture.Application;
            Configure(hostFixture.Configuration, hostFixture.Environment);
        }

        /// <summary>
        /// Gets the <see cref="IApplicationBuilder"/> initialized by the <see cref="IHost"/>.
        /// </summary>
        /// <value>The <see cref="IApplicationBuilder"/> initialized by the <see cref="IHost"/>.</value>
        public IApplicationBuilder Application { get; protected set; }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The type that provides the mechanisms to configure the HTTP request pipeline.</param>
        public abstract void ConfigureApplication(IApplicationBuilder app);
    }
}
