using System;
#if NETSTANDARD2_0_OR_GREATER
using System.Threading.Tasks;
#endif

namespace Codebelt.Extensions.Xunit
{
#if NETSTANDARD2_0_OR_GREATER
    public partial interface ITest
    {
        /// <summary>
        /// Asynchronously releases the resources used by the <see cref="Test"/>.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous dispose operation.</returns>
        ValueTask DisposeAsync();
    }
#else
    /// <seealso cref="IAsyncDisposable"/>
    public partial interface ITest : IAsyncDisposable
    {
    }
#endif

    /// <summary>
    /// Represents the members needed for vanilla testing.
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public partial interface ITest : IDisposable
    {
        /// <summary>
        /// Gets the type of caller for this instance. Default is <see cref="object.GetType"/>.
        /// </summary>
        /// <value>The type of caller for this instance.</value>
        Type CallerType { get; }
    }
}
