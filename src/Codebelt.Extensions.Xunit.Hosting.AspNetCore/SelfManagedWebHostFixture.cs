using System.Threading.Tasks;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Represents a self-managed implementation of the <see cref="ManagedWebHostFixture"/> class.
    /// </summary>
    public sealed class SelfManagedWebHostFixture : ManagedWebHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfManagedWebHostFixture"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor sets the <see cref="HostFixture.AsyncHostRunnerCallback"/> to a no-op asynchronous delegate.
        /// </remarks>
        public SelfManagedWebHostFixture()
        {
            AsyncHostRunnerCallback = (_, __) => Task.CompletedTask;
        }
    }
}
