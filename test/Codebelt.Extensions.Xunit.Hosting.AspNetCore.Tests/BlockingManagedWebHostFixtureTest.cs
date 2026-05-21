using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    public class BlockingManagedWebHostFixtureTest : Test
    {
        public BlockingManagedWebHostFixtureTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Create_ShouldSucceed_WhenBlockingManagedWebHostFixtureIsUsed()
        {
            using var startup = WebHostTestFactory.Create(hostFixture: new BlockingManagedWebHostFixture());

            Assert.NotNull(startup.Host);
        }
    }
}
