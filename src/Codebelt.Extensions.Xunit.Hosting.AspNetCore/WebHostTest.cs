using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Represents a base class from which all implementations of unit testing, that uses Microsoft Dependency Injection and depends on ASP.NET Core, should derive.
    /// </summary>
    /// <typeparam name="T">The type of the object that implements the <see cref="IWebHostFixture"/> interface.</typeparam>
    /// <seealso cref="Test" />
    /// <seealso cref="HostTest{T}" />
    public abstract class WebHostTest<T> : HostTest<T>, IWebHostTest where T : class, IWebHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebHostTest{T}"/> class.
        /// </summary>
        /// <param name="hostFixture">An implementation of the <see cref="IWebHostFixture"/> interface.</param>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        protected WebHostTest(T hostFixture, ITestOutputHelper output = null, Type callerType = null) : this(false, hostFixture, output, callerType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebHostTest{T}"/> class.
        /// </summary>
        /// <param name="skipHostFixtureInitialization">A value indicating whether to skip the host fixture initialization.</param>
        /// <param name="hostFixture">An implementation of the <see cref="IWebHostFixture"/> interface.</param>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        protected WebHostTest(bool skipHostFixtureInitialization, T hostFixture, ITestOutputHelper output = null, Type callerType = null) : base(skipHostFixtureInitialization, hostFixture, output, callerType)
        {
            ArgumentNullException.ThrowIfNull(hostFixture);
            if (skipHostFixtureInitialization) { return; }
            if (!hostFixture.HasValidState())
            {
                hostFixture.ConfigureHostCallback = ConfigureHost;
                hostFixture.ConfigureCallback = Configure;
                hostFixture.ConfigureServicesCallback = ConfigureServices;
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
