using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Extends the default implementation of the <see cref="IWebHostFixture"/> interface to be synchronous e.g., blocking where exceptions can be captured.
    /// </summary>
    /// <seealso cref="WebHostFixture" />
    public class BlockingWebHostFixture : WebHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockingWebHostFixture"/> class.
        /// </summary>
        public BlockingWebHostFixture()
        {
            HostRunnerCallback = host => host.Run();
        }
    }
}
