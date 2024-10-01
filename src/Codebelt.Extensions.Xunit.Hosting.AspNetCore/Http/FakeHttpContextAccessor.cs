using System;
using System.IO;
using System.Text;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features;
using Cuemon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http
{
    /// <summary>
    /// Provides a unit test implementation of <see cref="IHttpContextAccessor"/>.
    /// </summary>
    /// <seealso cref="IHttpContextAccessor" />
    public class FakeHttpContextAccessor : IHttpContextAccessor
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeHttpContextAccessor"/> class.
        /// </summary>
        public FakeHttpContextAccessor()
        {
            var fc = new FeatureCollection();
            fc.Set<IHttpResponseFeature>(new FakeHttpResponseFeature());
            fc.Set<IHttpRequestFeature>(new FakeHttpRequestFeature());
            fc.Set<IHttpResponseBodyFeature>(new StreamResponseBodyFeature(MakeGreeting("Hello awesome developers!")));
            HttpContext = new DefaultHttpContext(fc);
        }

        private Stream MakeGreeting(string greeting)
        {
            return Patterns.SafeInvoke(() => new MemoryStream(), ms =>
            {
                var sw = new StreamWriter(ms, Encoding.UTF8);
                sw.Write(greeting);
                sw.Flush();
                ms.Flush();
                ms.Position = 0;
                return ms;
            }, ex => throw new InvalidOperationException("There is an error in the Stream being written.", ex));
        }

        /// <summary>
        /// Gets or sets the HTTP context.
        /// </summary>
        /// <value>The HTTP context.</value>
        public HttpContext HttpContext { get; set; }
    }
}
