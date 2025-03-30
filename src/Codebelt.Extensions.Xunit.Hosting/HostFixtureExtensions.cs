namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Extension methods for the <see cref="IHostFixture"/> interface.
    /// </summary>
    public static class HostFixtureExtensions
    {
        /// <summary>
        /// Determines whether the specified <see cref="IHostFixture"/> has a valid state.
        /// </summary>
        /// <param name="fixture">The <see cref="IHostFixture"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="IHostFixture"/> has a valid state; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// A valid state is defined as having non-null values for the following properties:
        /// <see cref="IHostFixture.ConfigureServicesCallback"/>, <see cref="IHostFixture.Host"/>, 
        /// <see cref="IHostFixture.ServiceProvider"/> and <see cref="IHostFixture.ConfigureHostCallback"/>.
        /// </remarks>
        public static bool HasValidState(this IHostFixture fixture)
        {
            return fixture.ConfigureServicesCallback != null && 
                   fixture.Host != null && 
                   fixture.ServiceProvider != null && 
                   fixture.ConfigureHostCallback != null;
        }
    }
}
