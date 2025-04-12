namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Extension methods for the <see cref="IWebMinimalHostFixture"/> interface.
    /// </summary>
    public static class WebMinimalHostFixtureExtensions
    {
        /// <summary>
        /// Determines whether the specified <see cref="IWebMinimalHostFixture"/> has a valid state.
        /// </summary>
        /// <param name="hostFixture">The <see cref="IWebMinimalHostFixture"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="IWebMinimalHostFixture"/> has a valid state; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// A valid state is defined as having non-null values for the following properties:
        /// <see cref="IMinimalHostFixture.ConfigureHostCallback"/>, <see cref="IHostFixture.ConfigureCallback"/>,
        /// <see cref="IWebMinimalHostFixture.ConfigureApplicationCallback"/>, <see cref="IPipelineTest.Application"/> and <see cref="IHostFixture.Host"/>.
        /// </remarks>
        public static bool HasValidState(this IWebMinimalHostFixture hostFixture)
        {
            var hasValidState = ((IMinimalHostFixture)hostFixture).HasValidState();
            return hasValidState && hostFixture.ConfigureApplicationCallback != null && hostFixture.Application != null;
        }
    }
}
