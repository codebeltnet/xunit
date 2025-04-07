using System;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IMinimalHostFixture"/> interface.
    /// </summary>
    /// <seealso cref="IMinimalHostFixture" />
    /// <seealso cref="HostFixture"/>
    /// <remarks>This is the "modern" minimal style implementation of <see cref="GenericHostFixture"/>.</remarks>
    public class MinimalHostFixture : HostFixture, IMinimalHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinimalHostFixture"/> class.
        /// </summary>
        public MinimalHostFixture()
        {
        }

        /// <summary>
        /// Creates and configures the <see cref="IHost" /> of this instance.
        /// </summary>
        /// <param name="hostTest">The object that inherits from <see cref="MinimalHostTest{T}"/>.</param>
        /// <remarks><paramref name="hostTest"/> was added to support those cases where the caller is required in the host configuration.</remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="hostTest"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="hostTest"/> is not assignable from <see cref="MinimalHostTest{T}"/>.
        /// </exception>
        public virtual void ConfigureHost(Test hostTest)
        {
            if (hostTest == null) { throw new ArgumentNullException(nameof(hostTest)); }
            if (!HasTypes(hostTest.GetType(), typeof(MinimalHostTest<>))) { throw new ArgumentOutOfRangeException(nameof(hostTest), typeof(MinimalHostTest<>), $"{nameof(hostTest)} is not assignable from MinimalHostTest<T>."); }

            var hb = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(new HostApplicationBuilderSettings()
            {
                EnvironmentName = "Development",
                ApplicationName = hostTest.CallerType.Assembly.GetName().Name,
            });

            Configuration = hb.Configuration;
            Environment = hb.Environment;

            ConfigureCallback(Configuration, Environment);

            ConfigureHostCallback(hb);

            Host = hb.Build();

            HostRunnerCallback(Host);
        }

        /// <summary>
        /// Gets or sets the delegate that initializes the host application builder.
        /// </summary>
        /// <value>The delegate that initializes the host application builder.</value>
        public Action<IHostApplicationBuilder> ConfigureHostCallback { get; set; }
    }
}
