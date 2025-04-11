using System.Threading.Tasks;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Represents a self-managed implementation of the <see cref="ManagedHostFixture"/> class.
    /// </summary>
    public sealed class SelfManagedHostFixture : ManagedHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfManagedHostFixture"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor sets the <see cref="HostFixture.AsyncHostRunnerCallback"/> to a no-op asynchronous delegate.
        /// </remarks>
        public SelfManagedHostFixture()
        {
            AsyncHostRunnerCallback = (_, __) => Task.CompletedTask;
        }
    }
}
