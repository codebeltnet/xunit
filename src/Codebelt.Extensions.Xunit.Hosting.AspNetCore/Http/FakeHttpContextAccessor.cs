using System;
using System.IO;
using System.Text;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features;
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
            Stream interim = null;
            Stream result = null;
            try
            {
                interim = new MemoryStream();

                using (var sw = new StreamWriter(interim, Encoding.UTF8, leaveOpen: true))
                {
                    sw.Write(greeting);
                    sw.Flush();
                }

                interim.Flush();
                interim.Position = 0;

                result = interim;
                interim = null;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("There is an error in the Stream being written.", ex);
            }
            finally
            {
                interim?.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Gets or sets the HTTP context.
        /// </summary>
        /// <value>The HTTP context.</value>
        public HttpContext HttpContext { get; set; }
    }
}
