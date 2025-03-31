using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Provides a set of static methods for ASP.NET Core (including, but not limited to MVC, Razor and related) unit testing.
    /// </summary>
    /// <seealso cref="IHostTest"/>.
    public static class WebHostTestFactory
    {
        /// <summary>
        /// Creates and returns an <see cref="IWebHostTest"/> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection"/> which may be configured.</param>
        /// <param name="pipelineSetup">The <see cref="IApplicationBuilder"/> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <returns>An instance of an <see cref="IWebHostTest"/> implementation.</returns>
        [Obsolete("This method is obsolete and will be removed in a future version. Use Create(Action<IServiceCollection>, Action<IApplicationBuilder>, Action<IHostBuilder>, IAspNetCoreHostFixture) instead.")]
        public static IWebHostTest Create(Action<IServiceCollection> serviceSetup = null, Action<IApplicationBuilder> pipelineSetup = null, Action<IHostBuilder> hostSetup = null)
        {
            return Create(serviceSetup, pipelineSetup, hostSetup, null);
        }

        /// <summary>
        /// Creates and returns an <see cref="IWebHostTest"/> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection"/> which may be configured.</param>
        /// <param name="pipelineSetup">The <see cref="IApplicationBuilder"/> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="hostFixture">An optional <see cref="IAspNetCoreHostFixture"/> implementation to use instead of the default <see cref="AspNetCoreHostFixture"/> instance.</param>
        /// <returns>An instance of an <see cref="IWebHostTest"/> implementation.</returns>
        public static IWebHostTest Create(Action<IServiceCollection> serviceSetup = null, Action<IApplicationBuilder> pipelineSetup = null, Action<IHostBuilder> hostSetup = null, IAspNetCoreHostFixture hostFixture = null)
        {
            return new WebHostTest(serviceSetup, pipelineSetup, hostSetup, hostFixture ?? new AspNetCoreHostFixture());
        }

        /// <summary>
        /// Creates and returns an <see cref="IWebHostTest"/> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection"/> which may be configured.</param>
        /// <param name="pipelineSetup">The <see cref="IApplicationBuilder"/> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <returns>An instance of an <see cref="IWebHostTest"/> implementation.</returns>
        [Obsolete("This method is obsolete and will be removed in a future version. Use CreateWithHostBuilderContext(Action<HostBuilderContext, IServiceCollection>, Action<HostBuilderContext, IApplicationBuilder>, Action<IHostBuilder>, IAspNetCoreHostFixture) instead.")]
        public static IWebHostTest CreateWithHostBuilderContext(Action<HostBuilderContext, IServiceCollection> serviceSetup = null, Action<HostBuilderContext, IApplicationBuilder> pipelineSetup = null, Action<IHostBuilder> hostSetup = null)
        {
            return CreateWithHostBuilderContext(serviceSetup, pipelineSetup, hostSetup, null);
        }

        /// <summary>
        /// Creates and returns an <see cref="IWebHostTest"/> implementation.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection"/> which may be configured.</param>
        /// <param name="pipelineSetup">The <see cref="IApplicationBuilder"/> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="hostFixture">An optional <see cref="IAspNetCoreHostFixture"/> implementation to use instead of the default <see cref="AspNetCoreHostFixture"/> instance.</param>
        /// <returns>An instance of an <see cref="IWebHostTest"/> implementation.</returns>
        public static IWebHostTest CreateWithHostBuilderContext(Action<HostBuilderContext, IServiceCollection> serviceSetup = null, Action<HostBuilderContext, IApplicationBuilder> pipelineSetup = null, Action<IHostBuilder> hostSetup = null, IAspNetCoreHostFixture hostFixture = null)
        {
            return new WebHostTest(serviceSetup, pipelineSetup, hostSetup, hostFixture ?? new AspNetCoreHostFixture());
        }

        /// <summary>
        /// Runs a middleware and returns an <see cref="HttpClient"/> for making HTTP requests to the test server.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection"/> which may be configured.</param>
        /// <param name="pipelineSetup">The <see cref="IApplicationBuilder"/> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="responseFactory">The function delegate that creates a <see cref="HttpResponseMessage"/> from the <see cref="HttpClient"/>. Default is a GET request to the root URL ("/").</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains the <see cref="HttpResponseMessage"/> for the test server.</returns>
        [Obsolete("This method is obsolete and will be removed in a future version. Use RunAsync(Action<IServiceCollection>, Action<IApplicationBuilder>, Action<IHostBuilder>, Func<HttpClient, Task<HttpResponseMessage>>, IAspNetCoreHostFixture) instead.")]
        public static Task<HttpResponseMessage> RunAsync(Action<IServiceCollection> serviceSetup = null, Action<IApplicationBuilder> pipelineSetup = null, Action<IHostBuilder> hostSetup = null, Func<HttpClient, Task<HttpResponseMessage>> responseFactory = null)
        {
            return RunAsync(serviceSetup, pipelineSetup, hostSetup, responseFactory, null);
        }

        /// <summary>
        /// Runs a middleware and returns an <see cref="HttpClient"/> for making HTTP requests to the test server.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection"/> which may be configured.</param>
        /// <param name="pipelineSetup">The <see cref="IApplicationBuilder"/> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder"/> which may be configured.</param>
        /// <param name="responseFactory">The function delegate that creates a <see cref="HttpResponseMessage"/> from the <see cref="HttpClient"/>. Default is a GET request to the root URL ("/").</param>
        /// <param name="hostFixture">An optional <see cref="IAspNetCoreHostFixture"/> implementation to use instead of the default <see cref="AspNetCoreHostFixture"/> instance.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains the <see cref="HttpResponseMessage"/> for the test server.</returns>
        public static async Task<HttpResponseMessage> RunAsync(Action<IServiceCollection> serviceSetup = null, Action<IApplicationBuilder> pipelineSetup = null, Action<IHostBuilder> hostSetup = null, Func<HttpClient, Task<HttpResponseMessage>> responseFactory = null, IAspNetCoreHostFixture hostFixture = null)
        {
            using var client = Create(serviceSetup, pipelineSetup, hostSetup, hostFixture).Host.GetTestClient();
            return await client.ToHttpResponseMessageAsync(responseFactory).ConfigureAwait(false);
        }

        /// <summary>
        /// Runs a filter/middleware test.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="pipelineSetup">The <see cref="IApplicationBuilder" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder" /> which may be configured.</param>
        /// <param name="responseFactory">The function delegate that creates a <see cref="HttpResponseMessage"/> from the <see cref="HttpClient"/>. Default is a GET request to the root URL ("/").</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains the <see cref="HttpResponseMessage"/> for the test server.</returns>
        [Obsolete("This method is obsolete and will be removed in a future version. Use RunWithHostBuilderContextAsync(Action<HostBuilderContext, IServiceCollection>, Action<HostBuilderContext, IApplicationBuilder>, Action<IHostBuilder>, Func<HttpClient, Task<HttpResponseMessage>>, IAspNetCoreHostFixture) instead.")]
        public static Task<HttpResponseMessage> RunWithHostBuilderContextAsync(Action<HostBuilderContext, IServiceCollection> serviceSetup = null, Action<HostBuilderContext, IApplicationBuilder> pipelineSetup = null, Action<IHostBuilder> hostSetup = null, Func<HttpClient, Task<HttpResponseMessage>> responseFactory = null)
        {
            return RunWithHostBuilderContextAsync(serviceSetup, pipelineSetup, hostSetup, responseFactory, null);
        }

        /// <summary>
        /// Runs a filter/middleware test.
        /// </summary>
        /// <param name="serviceSetup">The <see cref="IServiceCollection" /> which may be configured.</param>
        /// <param name="pipelineSetup">The <see cref="IApplicationBuilder" /> which may be configured.</param>
        /// <param name="hostSetup">The <see cref="IHostBuilder" /> which may be configured.</param>
        /// <param name="responseFactory">The function delegate that creates a <see cref="HttpResponseMessage"/> from the <see cref="HttpClient"/>. Default is a GET request to the root URL ("/").</param>
        /// <param name="hostFixture">An optional <see cref="IAspNetCoreHostFixture"/> implementation to use instead of the default <see cref="AspNetCoreHostFixture"/> instance.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains the <see cref="HttpResponseMessage"/> for the test server.</returns>
        public static async Task<HttpResponseMessage> RunWithHostBuilderContextAsync(Action<HostBuilderContext, IServiceCollection> serviceSetup = null, Action<HostBuilderContext, IApplicationBuilder> pipelineSetup = null, Action<IHostBuilder> hostSetup = null, Func<HttpClient, Task<HttpResponseMessage>> responseFactory = null, IAspNetCoreHostFixture hostFixture = null)
        {
            using var client = CreateWithHostBuilderContext(serviceSetup, pipelineSetup, hostSetup, hostFixture).Host.GetTestClient();
            return await client.ToHttpResponseMessageAsync(responseFactory).ConfigureAwait(false);
        }
    }
}
