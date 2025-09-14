using System;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.Assets;
using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting
{
    public class MinimalHostFixtureTest : Test
    {
        private readonly ManagedMinimalHostFixture _hostFixture;

        public MinimalHostFixtureTest(ITestOutputHelper output) : base(output)
        {
            _hostFixture = new ManagedMinimalHostFixture();
        }

        [Fact]
        public void ConfigureHost_ShouldThrowArgumentNullException_WhenHostTestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _hostFixture.ConfigureHost(null));
        }

        [Fact]
        public void ConfigureHost_ShouldThrowArgumentOutOfRangeException_WhenHostTestIsNotAssignableFromHostTest()
        {
            var invalidHostTest = new InvalidHostTest<ManagedHostFixture>(new ManagedHostFixture());
            Assert.Throws<ArgumentOutOfRangeException>(() => _hostFixture.ConfigureHost(invalidHostTest));
        }

        [Fact]
        public void ConfigureHost_ShouldConfigureHostSuccessfully()
        {
            var validHostTest = new MinimalValidHostTest(_hostFixture);

            _hostFixture.ConfigureHost(validHostTest);

            Assert.NotNull(_hostFixture.Host);
            Assert.NotNull(_hostFixture.Host.Services);
            Assert.NotNull(_hostFixture.Configuration);
            Assert.NotNull(_hostFixture.Environment);
        }

        [Fact]
        public void Dispose_ShouldDisposeResources()
        {
            _hostFixture.Dispose();
            Assert.True(_hostFixture.Disposed);
        }

        [Fact]
        public async Task DisposeAsync_ShouldDisposeResourcesAsync()
        {
            await _hostFixture.DisposeAsync();
            Assert.True(_hostFixture.Disposed);
        }
    }
}
