using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Codebelt.Extensions.Xunit.Hosting;

/// <summary>
/// Provides a blocking managed implementation of the <see cref="IApplicationFixture{TEntryPoint}"/> interface.
/// </summary>
/// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
/// <seealso cref="HostFixture" />
/// <seealso cref="IApplicationFixture{TEntryPoint}" />
/// <remarks>
/// Unlike the base managed host fixtures, this fixture starts the resolved application host synchronously.
/// Application entry point testing must expose a fully started host after fixture initialization.
/// </remarks>
public class BlockingManagedApplicationFixture<TEntryPoint> : HostFixture, IApplicationFixture<TEntryPoint> where TEntryPoint : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockingManagedApplicationFixture{TEntryPoint}"/> class.
    /// </summary>
    public BlockingManagedApplicationFixture()
    {
        AsyncHostRunnerCallback = (host, _) =>
        {
            host.Start();
            return Task.CompletedTask;
        };
    }

    /// <summary>
    /// Creates and configures the <see cref="IHost" /> of this instance.
    /// </summary>
    /// <param name="hostTest">The object that inherits from <see cref="ApplicationTest{TEntryPoint,T}"/>.</param>
    /// <remarks><paramref name="hostTest"/> was added to support those cases where the caller is required in the host configuration.</remarks>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="hostTest"/> is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="hostTest"/> is not assignable from <see cref="ApplicationTest{TEntryPoint,T}"/>.
    /// </exception>
    public virtual void ConfigureHost(Test hostTest)
    {
#if NETSTANDARD2_0
        if (hostTest == null) { throw new ArgumentNullException(nameof(hostTest)); }
#else
        ArgumentNullException.ThrowIfNull(hostTest);
#endif
        if (!HasTypes(hostTest.GetType(), typeof(ApplicationTest<,>))) { throw new ArgumentOutOfRangeException(nameof(hostTest), typeof(ApplicationTest<,>), $"{nameof(hostTest)} is not assignable from ApplicationTest<TEntryPoint, T>."); }
        if (this.HasValidState()) { return; }

        Host = ApplicationHostFactory.Create<TEntryPoint>(ConfigureHostCallback);
        Configuration = Host.Services.GetRequiredService<IConfiguration>();
        Environment = Host.Services.GetRequiredService<IHostEnvironment>();

        ConfigureCallback(Configuration, Environment);

        AsyncHostRunnerCallback(Host, CancellationToken.None);
    }

    /// <summary>
    /// Gets or sets the delegate that provides a way to override the <see cref="IHostBuilder"/> before the application is built.
    /// </summary>
    /// <value>The delegate that provides a way to override the <see cref="IHostBuilder"/>.</value>
    public Action<IHostBuilder> ConfigureHostCallback { get; set; }
}
