namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Extension methods for the <see cref="IWebHostFixture"/> interface.
    /// </summary>
    public static class WebHostFixtureExtensions
    {
        /// <summary>
        /// Determines whether the specified <see cref="IWebHostFixture"/> has a valid state.
        /// </summary>
        /// <param name="hostFixture">The <see cref="IWebHostFixture"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="IWebHostFixture"/> has a valid state; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// A valid state is defined as having non-null values for the following properties:
        /// <see cref="IWebHostFixture.ConfigureApplicationCallback"/>, <see cref="IGenericHostFixture.ConfigureServicesCallback"/>, <see cref="IHostTest.Host"/>, 
        /// <see cref="IGenericHostFixture.ConfigureHostCallback"/> and <see cref="IPipelineTest.Application"/>.
        /// </remarks>
        public static bool HasValidState(this IWebHostFixture hostFixture)
        {
            var hasValidState = ((IGenericHostFixture)hostFixture).HasValidState();
            return hasValidState && hostFixture.ConfigureApplicationCallback != null && hostFixture.Application != null;
        }
    }
}
