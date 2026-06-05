namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

/// <summary>
/// Extension methods for the <see cref="IWebApplicationFixture{TEntryPoint}"/> interface.
/// </summary>
public static class WebApplicationFixtureExtensions
{
    /// <summary>
    /// Determines whether the specified <see cref="IWebApplicationFixture{TEntryPoint}"/> has a valid state.
    /// </summary>
    /// <typeparam name="TEntryPoint">A type in the entry point assembly of the application.</typeparam>
    /// <param name="hostFixture">The <see cref="IWebApplicationFixture{TEntryPoint}"/> to check.</param>
    /// <returns><c>true</c> if the specified <see cref="IWebApplicationFixture{TEntryPoint}"/> has a valid state; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// A valid state is defined as having non-null values for the following properties:
    /// <see cref="IHostFixture.Host"/>, <see cref="IHostFixture.ConfigureCallback"/> and <see cref="IWebApplicationFixture{TEntryPoint}.ConfigureWebHostCallback"/>.
    /// </remarks>
    public static bool HasValidState<TEntryPoint>(this IWebApplicationFixture<TEntryPoint> hostFixture) where TEntryPoint : class
    {
        return hostFixture.Host != null &&
               hostFixture.ConfigureCallback != null &&
               hostFixture.ConfigureWebHostCallback != null;
    }
}
