namespace Codebelt.Extensions.Xunit.Hosting;

/// <summary>
/// Extension methods for the <see cref="IApplicationFixture{TEntryPoint}"/> interface.
/// </summary>
public static class ApplicationFixtureExtensions
{
    /// <summary>
    /// Determines whether the specified <see cref="IApplicationFixture{TEntryPoint}"/> has a valid state.
    /// </summary>
    /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
    /// <param name="hostFixture">The <see cref="IApplicationFixture{TEntryPoint}"/> to check.</param>
    /// <returns><c>true</c> if the specified <see cref="IApplicationFixture{TEntryPoint}"/> has a valid state; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// A valid state is defined as having non-null values for the following properties:
    /// <see cref="IHostFixture.Host"/>, <see cref="IHostFixture.ConfigureCallback"/> and <see cref="IApplicationFixture{TEntryPoint}.ConfigureHostCallback"/>.
    /// </remarks>
    public static bool HasValidState<TEntryPoint>(this IApplicationFixture<TEntryPoint> hostFixture) where TEntryPoint : class
    {
        return hostFixture.Host != null &&
               hostFixture.ConfigureCallback != null &&
               hostFixture.ConfigureHostCallback != null;
    }
}
