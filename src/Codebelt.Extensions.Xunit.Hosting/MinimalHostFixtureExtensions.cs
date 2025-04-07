namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Extension methods for the <see cref="IMinimalHostFixture"/> interface.
    /// </summary>
    public static class MinimalHostFixtureExtensions
    {
        /// <summary>
        /// Determines whether the specified <see cref="IMinimalHostFixture"/> has a valid state.
        /// </summary>
        /// <param name="hostFixture">The <see cref="IMinimalHostFixture"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="IMinimalHostFixture"/> has a valid state; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// A valid state is defined as having non-null values for the following properties:
        /// <see cref="IMinimalHostFixture.ConfigureHostCallback"/>, <see cref="IHostFixture.ConfigureCallback"/> and <see cref="IHostFixture.Host"/>.
        /// </remarks>
        public static bool HasValidState(this IMinimalHostFixture hostFixture)
        {
            return hostFixture.Host != null && 
                   hostFixture.ConfigureHostCallback != null &&
                   hostFixture.ConfigureCallback != null;
        }
    }
}
