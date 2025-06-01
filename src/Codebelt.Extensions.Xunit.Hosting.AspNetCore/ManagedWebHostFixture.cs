using System;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IWebHostFixture"/> interface.
    /// </summary>
    /// <seealso cref="ManagedHostFixture" />
    /// <seealso cref="IWebHostFixture" />
    public class ManagedWebHostFixture : ManagedHostFixture, IWebHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedWebHostFixture"/> class.
        /// </summary>
        public ManagedWebHostFixture()
        {
        }

        /// <summary>
        /// Creates and configures the <see cref="IWebHost" /> of this instance.
        /// </summary>
        /// <param name="hostTest">The object that inherits from <see cref="WebHostTest{T}" />.</param>
        /// <remarks><paramref name="hostTest" /> was added to support those cases where the caller is required in the host configuration.</remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hostTest"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="hostTest"/> is not assignable from <see cref="WebHostTest{T}"/>.
        /// </exception>
        public override void ConfigureHost(Test hostTest)
        {
            ArgumentNullException.ThrowIfNull(hostTest);
            if (!HasTypes(hostTest.GetType(), typeof(WebHostTest<>))) { throw new ArgumentOutOfRangeException(nameof(hostTest), typeof(WebHostTest<>), $"{nameof(hostTest)} is not assignable from WebHostTest<T>."); }
            if (!this.HasValidState()) { return; } // had to include this due to dual-call this method (one uncontrolled from xUnit library reflection magic; second controlled from this library)

            var hb = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer(o => o.PreserveExecutionContext = true)
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseEnvironment("Development")
                        .ConfigureAppConfiguration((context, config) =>
                        {
                            config.AddJsonFile("appsettings.json", true, true)
                                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
                                .AddEnvironmentVariables();

                            StaticWebAssetsLoader.UseStaticWebAssets(context.HostingEnvironment, context.Configuration);

                            ConfigureCallback(config.Build(), context.HostingEnvironment);
                        })
                        .ConfigureLogging((context, logging) =>
                        {
                            logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                            logging.AddConsole();
                            logging.AddDebug();
                            logging.AddEventSourceLogger();
                        })
                        .ConfigureServices((context, services) =>
                        {
                            Configuration = context.Configuration;
                            Environment = context.HostingEnvironment;
                            ConfigureServicesCallback(services);
                        })
                        .Configure(app =>
                            {
                                ConfigureApplicationCallback.Invoke(app);
                                Application = app;
                            }
                        )
                        .UseSetting(HostDefaults.ApplicationKey, hostTest.CallerType.Assembly.GetName().Name);
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
        /// Gets or sets the delegate that configures the HTTP request pipeline.
        /// </summary>
        /// <value>The delegate that configures the HTTP request pipeline.</value>
        public Action<IApplicationBuilder> ConfigureApplicationCallback { get; set; }

        /// <summary>
        /// Gets the <see cref="IApplicationBuilder" /> initialized by the <see cref="IHost" />.
        /// </summary>
        /// <value>The <see cref="IApplicationBuilder" /> initialized by the <see cref="IHost" />.</value>
        public IApplicationBuilder Application { get; protected set; }
    }
}
