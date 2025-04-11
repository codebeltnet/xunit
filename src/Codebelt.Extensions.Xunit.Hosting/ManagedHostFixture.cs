using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IGenericHostFixture"/> interface.
    /// </summary>
    /// <seealso cref="HostFixture"/>
    /// <seealso cref="IGenericHostFixture" />
    public class ManagedHostFixture : HostFixture, IGenericHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedHostFixture"/> class.
        /// </summary>
        public ManagedHostFixture()
        {
        }

        /// <summary>
        /// Creates and configures the <see cref="IHost" /> of this instance.
        /// </summary>
        /// <param name="hostTest">The object that inherits from <see cref="HostTest{T}"/>.</param>
        /// <remarks><paramref name="hostTest"/> was added to support those cases where the caller is required in the host configuration.</remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hostTest"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="hostTest"/> is not assignable from <see cref="HostTest{T}"/>.
        /// </exception>
        public virtual void ConfigureHost(Test hostTest)
        {
            if (hostTest == null) { throw new ArgumentNullException(nameof(hostTest)); }
            if (!HasTypes(hostTest.GetType(), typeof(HostTest<>))) { throw new ArgumentOutOfRangeException(nameof(hostTest), typeof(HostTest<>), $"{nameof(hostTest)} is not assignable from HostTest<T>."); }

            var hb = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment("Development")
                .ConfigureAppConfiguration((context, config) =>
                {
                    config
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
                        .AddEnvironmentVariables();

                    ConfigureCallback(config.Build(), context.HostingEnvironment);
                })
                .ConfigureServices((context, services) =>
                {
                    Configuration = context.Configuration;
                    Environment = context.HostingEnvironment;
                    ConfigureServicesCallback(services);
                })
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { HostDefaults.ApplicationKey, hostTest.CallerType.Assembly.GetName().Name }
                    });
                });

#if NET9_0_OR_GREATER
            hb.UseDefaultServiceProvider(o =>
            {
                o.ValidateOnBuild = true;
                o.ValidateScopes = true;
            });
#endif

            ConfigureHostCallback(hb);

            Host = hb.Build();

            AsyncHostRunnerCallback(Host, CancellationToken.None);
        }

        /// <summary>
        /// Gets or sets the delegate that initializes the host builder.
        /// </summary>
        /// <value>The delegate that initializes the host builder.</value>
        public Action<IHostBuilder> ConfigureHostCallback { get; set; }

        /// <summary>
        /// Gets or sets the delegate that adds services to the container.
        /// </summary>
        /// <value>The delegate that adds services to the container.</value>
        public Action<IServiceCollection> ConfigureServicesCallback { get; set; }
    }
}
