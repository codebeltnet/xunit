﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Represents a base class from which all implementations of unit testing, that uses Microsoft Dependency Injection, should derive.
    /// </summary>
    /// <typeparam name="T">The type of the object that implements the <see cref="IHostFixture"/> interface.</typeparam>
    /// <seealso cref="Test" />
    /// <seealso cref="IClassFixture{TFixture}" />
    /// <remarks>The class needed to be designed in this rather complex way, as this is the only way that xUnit supports a shared context. The need for shared context is theoretical at best, but it does opt-in for Scoped instances.</remarks>
    public abstract class HostTest<T> : Test, IGenericHostTest, IClassFixture<T> where T : class, IHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostTest{T}"/> class.
        /// </summary>
        /// <param name="hostFixture">An implementation of the <see cref="IHostFixture"/> interface.</param>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hostFixture"/> is null.
        /// </exception>
        protected HostTest(T hostFixture, ITestOutputHelper output = null, Type callerType = null) : this(false, hostFixture, output, callerType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostTest{T}"/> class.
        /// </summary>
        /// <param name="skipHostFixtureInitialization">A value indicating whether to skip the host fixture initialization.</param>
        /// <param name="hostFixture">An implementation of the <see cref="IHostFixture"/> interface.</param>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper"/> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hostFixture"/> is null.
        /// </exception>
        protected HostTest(bool skipHostFixtureInitialization, T hostFixture, ITestOutputHelper output = null, Type callerType = null) : base(output, callerType)
        {
            if (hostFixture == null) { throw new ArgumentNullException(nameof(hostFixture)); }
            if (skipHostFixtureInitialization) { return; }
            if (!hostFixture.HasValidState())
            {
                hostFixture.ConfigureHostCallback = ConfigureHost;
                hostFixture.ConfigureCallback = Configure;
                hostFixture.ConfigureServicesCallback = ConfigureServices;
                hostFixture.ConfigureHost(this);
            }
            Host = hostFixture.Host;
            ServiceProvider = hostFixture.Host.Services;
            Configure(hostFixture.Configuration, hostFixture.HostingEnvironment);
        }

        /// <summary> 
        /// Initializes the specified host fixture. 
        /// </summary> 
        /// <param name="hostFixture">The host fixture to initialize.</param>
        [Obsolete("This method is obsolete and will be removed in a future version. It remains only to support binary compatibility.")]
        protected virtual void InitializeHostFixture(T hostFixture)
        {
        }

        /// <summary>
        /// Gets the <see cref="IHost"/> initialized by the <see cref="IHostFixture"/>.
        /// </summary>
        /// <value>The <see cref="IHost"/> initialized by the <see cref="IHostFixture"/>.</value>
        public IHost Host { get; protected set; }

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> initialized by the <see cref="IHost"/>.
        /// </summary>
        /// <value>The <see cref="IServiceProvider"/> initialized by the <see cref="IHost"/>.</value>
        public IServiceProvider ServiceProvider { get; protected set; }

        /// <summary>
        /// Gets the <see cref="IConfiguration"/> initialized by the <see cref="IHost"/>.
        /// </summary>
        /// <value>The <see cref="IConfiguration"/> initialized by the <see cref="IHost"/>.</value>
        public IConfiguration Configuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the <see cref="IHostEnvironment"/> initialized by the <see cref="IHost"/>.
        /// </summary>
        /// <value>The <see cref="IHostEnvironment"/> initialized by the <see cref="IHost"/>.</value>
        public IHostEnvironment HostingEnvironment
        {
            get;
            private set;
        }

        /// <summary>
        /// Adds <see cref="Configuration"/> and <see cref="HostingEnvironment"/> to this instance.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/> initialized by the <see cref="IHost"/>.</param>
        /// <param name="environment">The <see cref="IHostEnvironment"/> initialized by the <see cref="IHost"/>.</param>
        public virtual void Configure(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            HostingEnvironment = environment;
        }

        /// <summary>
        /// Provides a way to override the <see cref="IHostBuilder"/> defaults set up by <typeparamref name="T"/>.
        /// </summary>
        /// <param name="hb">The <see cref="IHostBuilder"/> that initializes an instance of <see cref="IHost"/>.</param>
        protected virtual void ConfigureHost(IHostBuilder hb)
        {
        }

        /// <summary>
        /// Adds services to the container.
        /// </summary>
        /// <param name="services">The collection of service descriptors.</param>
        public abstract void ConfigureServices(IServiceCollection services);
    }
}
