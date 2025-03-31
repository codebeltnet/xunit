﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Provides extension methods for the <see cref="HttpClient"/> class.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Provides a convenient way to return a <see cref="HttpResponseMessage"/> from a <see cref="HttpClient"/> using the specified <paramref name="responseFactory"/>.
        /// </summary>
        /// <param name="client">The <see cref="HttpClient"/> to extend.</param>
        /// <param name="responseFactory">The function delegate that creates a <see cref="HttpResponseMessage"/> from the <see cref="HttpClient"/>. Default is a GET request to the root URL ("/").</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The task result contains the <see cref="HttpResponseMessage"/> generated by the <paramref name="responseFactory"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="client"/> cannot be null.
        /// </exception>
        /// <remarks>Designed to be used in conjunction with <see cref="WebHostTestFactory.RunAsync(System.Action{Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{Microsoft.AspNetCore.Builder.IApplicationBuilder},System.Action{Microsoft.Extensions.Hosting.IHostBuilder},System.Func{System.Net.Http.HttpClient,System.Threading.Tasks.Task{System.Net.Http.HttpResponseMessage}},IAspNetCoreHostFixture)"/> and <see cref="WebHostTestFactory.RunWithHostBuilderContextAsync(System.Action{Microsoft.Extensions.Hosting.HostBuilderContext,Microsoft.Extensions.DependencyInjection.IServiceCollection},System.Action{Microsoft.Extensions.Hosting.HostBuilderContext,Microsoft.AspNetCore.Builder.IApplicationBuilder},System.Action{Microsoft.Extensions.Hosting.IHostBuilder},System.Func{System.Net.Http.HttpClient,System.Threading.Tasks.Task{System.Net.Http.HttpResponseMessage}},IAspNetCoreHostFixture)"/>.</remarks>
        public static async Task<HttpResponseMessage> ToHttpResponseMessageAsync(this HttpClient client, Func<HttpClient, Task<HttpResponseMessage>> responseFactory = null)
        {
            if (client == null) { throw new ArgumentNullException(nameof(client)); }
            responseFactory ??= c => c.GetAsync("/");
            return await responseFactory(client).ConfigureAwait(false);
        }
    }
}
