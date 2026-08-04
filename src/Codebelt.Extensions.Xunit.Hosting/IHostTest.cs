using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting;

/// <summary>
/// Represents the members needed for bare-bone DI testing with support for <see cref="IHost" />.
/// </summary>
/// <seealso cref="IConfigurationTest" />
/// <seealso cref="IEnvironmentTest" />
/// <seealso cref="ITest" />
public interface IHostTest : IConfigurationTest, IEnvironmentTest, ITest
{
    /// <summary>
    /// Gets the <see cref="IHost"/> initialized by the <see cref="IGenericHostFixture"/>.
    /// </summary>
    /// <value>The <see cref="IHost"/> initialized by the <see cref="IGenericHostFixture"/>.</value>
    /// <remarks>For hosts captured by the opt-in managed application fixture path, accessing this property starts the deferred host when necessary.</remarks>
    IHost Host { get; }
}
