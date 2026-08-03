using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

/// <summary>
/// Provides a set of static methods for ASP.NET Core testing that bootstraps an existing application entry point.
/// </summary>
/// <seealso cref="IHostTest"/>.
public static class WebApplicationTestFactory
{
    /// <summary>
    /// Creates and returns an <see cref="IHostTest"/> implementation.
    /// </summary>
    /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
    /// <param name="webHostSetup">The <see cref="IWebHostBuilder"/> which may be configured.</param>
    /// <param name="hostFixture">An optional <see cref="IWebApplicationFixture{TEntryPoint}"/> implementation to use instead of the default <see cref="BlockingManagedWebApplicationFixture{TEntryPoint}"/> instance.</param>
    /// <returns>An instance of an <see cref="IHostTest"/> implementation.</returns>
    /// <remarks>
    /// Passing a <see cref="ManagedWebApplicationFixture{TEntryPoint}"/> opts this call into entrypoint-owned deferred startup. Omitting <paramref name="hostFixture"/> preserves the blocking compatibility path for the current minor release; that default should be removed or changed in the next major release.
    /// </remarks>
    public static IHostTest Create<TEntryPoint>(Action<IWebHostBuilder> webHostSetup = null, IWebApplicationFixture<TEntryPoint> hostFixture = null) where TEntryPoint : class
    {
        // Minor-release compatibility: keep the historical blocking default while allowing callers to opt in by passing ManagedWebApplicationFixture<TEntryPoint>.
        // Major release: remove or change this default when the compatibility fixture is retired. Hint: ManagedWebApplicationFixture
        return new Internal.WebApplicationTest<TEntryPoint>(webHostSetup, hostFixture ?? new BlockingManagedWebApplicationFixture<TEntryPoint>());
    }

    /// <summary>
    /// Runs an ASP.NET Core application and returns an <see cref="HttpResponseMessage"/> for the test server.
    /// </summary>
    /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
    /// <param name="webHostSetup">The <see cref="IWebHostBuilder"/> which may be configured.</param>
    /// <param name="responseFactory">The function delegate that creates a <see cref="HttpResponseMessage"/> from the <see cref="HttpClient"/>. Default is a GET request to the root URL ("/").</param>
    /// <param name="hostFixture">An optional <see cref="IWebApplicationFixture{TEntryPoint}"/> implementation to use instead of the default <see cref="BlockingManagedWebApplicationFixture{TEntryPoint}"/> instance.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains the <see cref="HttpResponseMessage"/> for the test server.</returns>
    public static async Task<HttpResponseMessage> RunAsync<TEntryPoint>(Action<IWebHostBuilder> webHostSetup = null, Func<HttpClient, Task<HttpResponseMessage>> responseFactory = null, IWebApplicationFixture<TEntryPoint> hostFixture = null) where TEntryPoint : class
    {
        using var client = Create<TEntryPoint>(webHostSetup, hostFixture).Host.GetTestClient();
        return await client.ToHttpResponseMessageAsync(responseFactory).ConfigureAwait(false);
    }
}
