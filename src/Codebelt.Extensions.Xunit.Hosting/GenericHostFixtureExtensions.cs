namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Extension methods for the <see cref="IGenericHostFixture"/> interface.
    /// </summary>
    public static class GenericHostFixtureExtensions
    {
        /// <summary>
        /// Determines whether the specified <see cref="IGenericHostFixture"/> has a valid state.
        /// </summary>
        /// <param name="hostFixture">The <see cref="IGenericHostFixture"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="IGenericHostFixture"/> has a valid state; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// A valid state is defined as having non-null values for the following properties:
        /// <see cref="IGenericHostFixture.ConfigureServicesCallback"/>, <see cref="IHostTest.Host"/> and <see cref="IGenericHostFixture.ConfigureHostCallback"/>.
        /// </remarks>
        public static bool HasValidState(this IGenericHostFixture hostFixture)
        {
            return hostFixture.ConfigureServicesCallback != null && 
                   hostFixture.Host != null && 
                   hostFixture.ConfigureHostCallback != null;
        }
    }
}
