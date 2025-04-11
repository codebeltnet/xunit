using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IWebMinimalHostFixture"/> interface.
    /// </summary>
    /// <seealso cref="ManagedMinimalHostFixture"/>
    /// <seealso cref="IWebMinimalHostFixture" />
    /// <remarks>This is the "modern" minimal style implementation of <see cref="ManagedWebHostFixture"/>.</remarks>
    public class ManagedWebMinimalHostFixture : ManagedMinimalHostFixture, IWebMinimalHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedWebMinimalHostFixture"/> class.
        /// </summary>
        public ManagedWebMinimalHostFixture()
        {
        }

        /// <summary>
        /// Creates and configures the <see cref="IHost" /> of this instance.
        /// </summary>
        /// <param name="hostTest">The object that inherits from <see cref="MinimalWebHostTest{T}"/>.</param>
        /// <remarks><paramref name="hostTest"/> was added to support those cases where the caller is required in the host configuration.</remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hostTest"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="hostTest"/> is not assignable from <see cref="MinimalWebHostTest{T}"/>.
        /// </exception>
        public override void ConfigureHost(Test hostTest)
        {
            if (hostTest == null) { throw new ArgumentNullException(nameof(hostTest)); }
            if (!HasTypes(hostTest.GetType(), typeof(MinimalWebHostTest<>))) { throw new ArgumentOutOfRangeException(nameof(hostTest), typeof(MinimalWebHostTest<>), $"{nameof(hostTest)} is not assignable from MinimalWebHostTest<T>."); }

            var hb = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                EnvironmentName = "Development",
                ApplicationName = hostTest.CallerType.Assembly.GetName().Name
            });

            hb.WebHost.UseTestServer(o => o.PreserveExecutionContext = true);

            Configuration = hb.Configuration;
            Environment = hb.Environment;

            ConfigureCallback(Configuration, Environment);

            ConfigureHostCallback(hb);

            var webApplication = hb.Build();

            ConfigureApplicationCallback(webApplication);
            Application = webApplication;
            
            Host = webApplication;

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
