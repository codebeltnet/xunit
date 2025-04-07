using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Represents the members needed for DI testing with support for <see cref="IHostEnvironment"/>.
    /// </summary>
    public interface IEnvironmentTest
    {
        /// <summary>
        /// Gets the <see cref="IHostEnvironment"/> initialized by the <see cref="IHost"/>.
        /// </summary>
        /// <value>The <see cref="IHostEnvironment"/> initialized by the <see cref="IHost"/>.</value>
        IHostEnvironment Environment { get; }
    }
}
