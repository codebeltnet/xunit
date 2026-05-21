using Microsoft.AspNetCore.Builder;
using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    public class SelfManagedWebMinimalHostFixtureTest : MinimalWebHostTest<SelfManagedWebMinimalHostFixture>
    {
        public SelfManagedWebMinimalHostFixtureTest(SelfManagedWebMinimalHostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
        {
        }

        [Fact]
        public void Host_ShouldNotBeNull_WhenNoOpRunnerIsUsed()
        {
            Assert.NotNull(Host);
            Assert.NotNull(Application);
        }

        public override void ConfigureApplication(IApplicationBuilder app)
        {
        }
    }
}
