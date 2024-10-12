using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IHostFixture"/> interface.
    /// </summary>
    /// <seealso cref="IHostFixture" />
    public class HostFixture : IDisposable, IHostFixture
    {
        private readonly Lock _lock = LockFactory.Create();

        /// <summary>
        /// Initializes a new instance of the <see cref="HostFixture"/> class.
        /// </summary>
        public HostFixture()
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
                    HostingEnvironment = context.HostingEnvironment;
                    ConfigureServicesCallback(services);
                });

#if NET9_0_OR_GREATER
            hb.UseDefaultServiceProvider(o =>
            {
                o.ValidateOnBuild = true;
                o.ValidateScopes = true;
            });
#endif

            ConfigureHostCallback(hb);

            var host = hb.Build();
            Task.Run(() => host.StartAsync().ConfigureAwait(false))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
            Host = host;
        }

        /// <summary>
        /// Determines whether the specified <paramref name="type"/> contains one or more of the specified target <paramref name="types"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to validate.</param>
        /// <param name="types">The target types to be matched against.</param>
        /// <returns><c>true</c> if the <paramref name="type"/> contains one or more of the specified target types; otherwise, <c>false</c>.</returns>
        protected static bool HasTypes(Type type, params Type[] types)
        {
            foreach (var tt in types)
            {
                var st = type;
                while (st != null)
                {
                    if (st.IsGenericType && tt == st.GetGenericTypeDefinition()) { return true; }
                    if (st == tt) { return true; }
                    st = st.BaseType;
                }
            }
            return false;
        }

#if NETSTANDARD2_0_OR_GREATER
        /// <summary>
        /// Gets or sets the delegate that initializes the test class.
        /// </summary>
        /// <value>The delegate that initializes the test class.</value>
        /// <remarks>Mimics the Startup convention.</remarks>
        public Action<IConfiguration, IHostingEnvironment> ConfigureCallback { get; set; }
#else
        /// <summary>
        /// Gets or sets the delegate that initializes the test class.
        /// </summary>
        /// <value>The delegate that initializes the test class.</value>
        /// <remarks>Mimics the Startup convention.</remarks>
        public Action<IConfiguration, IHostEnvironment> ConfigureCallback { get; set; }
#endif

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

        /// <summary>
        /// Gets or sets the <see cref="IHost" /> initialized by this instance.
        /// </summary>
        /// <value>The <see cref="IHost" /> initialized by this instance.</value>
        public IHost Host { get; protected set; }

        /// <summary>
        /// Gets the <see cref="IServiceProvider" /> initialized by this instance.
        /// </summary>
        /// <value>The <see cref="IServiceProvider" /> initialized by this instance.</value>
        public IServiceProvider ServiceProvider => Host?.Services;

        /// <summary>
        /// Gets the <see cref="IConfiguration" /> initialized by this instance.
        /// </summary>
        /// <value>The <see cref="IConfiguration" /> initialized by this instance.</value>
        public IConfiguration Configuration { get; protected set; }

#if NETSTANDARD2_0_OR_GREATER
        /// <summary>
        /// Gets the <see cref="IHostingEnvironment"/> initialized by this instance.
        /// </summary>
        /// <value>The <see cref="IHostingEnvironment"/> initialized by this instance.</value>
        public IHostingEnvironment HostingEnvironment { get; protected set; }
#else
        /// <summary>
        /// Gets the <see cref="IHostEnvironment"/> initialized by this instance.
        /// </summary>
        /// <value>The <see cref="IHostEnvironment"/> initialized by this instance.</value>
        public IHostEnvironment HostingEnvironment { get; protected set; }
#endif

        /// <summary>
        /// Gets a value indicating whether this <see cref="HostFixture"/> object is disposed.
        /// </summary>
        /// <value><c>true</c> if this <see cref="HostFixture"/> object is disposed; otherwise, <c>false</c>.</value>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Called when this object is being disposed by either <see cref="Dispose()" /> or <see cref="Dispose(bool)" /> having <c>disposing</c> set to <c>true</c> and <see cref="Disposed" /> is <c>false</c>.
        /// </summary>
        protected virtual void OnDisposeManagedResources()
        {
            if (ServiceProvider is ServiceProvider sp)
            {
                sp.Dispose();
            }
            Host?.Dispose();
        }

        /// <summary>
        /// Called when this object is being disposed by either <see cref="Dispose()"/> or <see cref="Dispose(bool)"/> and <see cref="Disposed"/> is <c>false</c>.
        /// </summary>
        protected virtual void OnDisposeUnmanagedResources()
        {
        }

        /// <summary>
        /// Releases all resources used by the <see cref="HostFixture"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="HostFixture"/> object and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            if (Disposed) { return; }
            lock (_lock)
            {
                if (Disposed) { return; }
                if (disposing)
                {
                    OnDisposeManagedResources();
                }
                OnDisposeUnmanagedResources();
                Disposed = true;
            }
        }
    }
}
