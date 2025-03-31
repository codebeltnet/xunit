namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Extension methods for the <see cref="IAspNetCoreHostFixture"/> interface.
    /// </summary>
    public static class AspNetCoreHostFixtureExtensions
    {
        /// <summary>
        /// Determines whether the specified <see cref="IAspNetCoreHostFixture"/> has a valid state.
        /// </summary>
        /// <param name="fixture">The <see cref="IAspNetCoreHostFixture"/> to check.</param>
        /// <returns><c>true</c> if the specified <see cref="IAspNetCoreHostFixture"/> has a valid state; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// A valid state is defined as having non-null values for the following properties:
        /// <see cref="IHostFixture.ConfigureServicesCallback"/>, <see cref="IHostTest.Host"/>, 
        /// <see cref="IServiceTest.ServiceProvider"/>, <see cref="IHostFixture.ConfigureHostCallback"/> and <see cref="IPipelineTest.Application"/>.
        /// </remarks>
        public static bool HasValidState(this IAspNetCoreHostFixture fixture)
        {
            var hasValidState = ((IHostFixture)fixture).HasValidState();
            return hasValidState && fixture.Application != null;
        }
    }
}
