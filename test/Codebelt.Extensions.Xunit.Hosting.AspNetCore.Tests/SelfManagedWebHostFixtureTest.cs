using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    public class SelfManagedWebHostFixtureTest : Test
    {
        public SelfManagedWebHostFixtureTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Create_ShouldSucceed_WhenSelfManagedWebHostFixtureIsUsed()
        {
            using var startup = WebHostTestFactory.Create(hostFixture: new SelfManagedWebHostFixture());

            Assert.NotNull(startup.Host);
        }
    }
}
