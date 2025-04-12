using System.Threading.Tasks;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Represents a self-managed implementation of the <see cref="ManagedMinimalHostFixture"/> class.
    /// </summary>
    public sealed class SelfManagedMinimalHostFixture : ManagedMinimalHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfManagedMinimalHostFixture"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor sets the <see cref="HostFixture.AsyncHostRunnerCallback"/> to a no-op asynchronous delegate.
        /// </remarks>
        public SelfManagedMinimalHostFixture()
        {
            AsyncHostRunnerCallback = (_, __) => Task.CompletedTask;
        }
    }
}
