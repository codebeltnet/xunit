using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Extends the default implementation of the <see cref="IWebHostFixture"/> interface to be synchronous e.g., blocking where exceptions can be captured.
    /// </summary>
    /// <seealso cref="ManagedWebHostFixture" />
    public sealed class BlockingManagedWebHostFixture : ManagedWebHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockingManagedWebHostFixture"/> class.
        /// </summary>
        public BlockingManagedWebHostFixture()
        {
            AsyncHostRunnerCallback = (host, _) =>
            {
                host.Start();
                return Task.CompletedTask;
            };
        }
    }
}
