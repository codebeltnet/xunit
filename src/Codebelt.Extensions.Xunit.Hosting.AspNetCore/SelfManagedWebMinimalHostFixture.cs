using System.Threading.Tasks;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Represents a self-managed implementation of the <see cref="ManagedMinimalHostFixture"/> class.
    /// </summary>
    public sealed class SelfManagedWebMinimalHostFixture : ManagedWebMinimalHostFixture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfManagedWebMinimalHostFixture"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor sets the <see cref="HostFixture.AsyncHostRunnerCallback"/> to a no-op asynchronous delegate.
        /// </remarks>
        public SelfManagedWebMinimalHostFixture()
        {
            AsyncHostRunnerCallback = (_, __) => Task.CompletedTask;
        }
    }
}
