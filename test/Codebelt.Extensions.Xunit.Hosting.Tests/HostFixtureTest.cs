using System;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.Assets;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting
{
    public class HostFixtureTest : Test
    {
        private readonly HostFixture _hostFixture;

        public HostFixtureTest(ITestOutputHelper output) : base(output)
        {
            _hostFixture = new HostFixture();
        }

        [Fact]
        public void ConfigureHost_ShouldThrowArgumentNullException_WhenHostTestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _hostFixture.ConfigureHost(null));
        }

        [Fact]
        public void ConfigureHost_ShouldThrowArgumentOutOfRangeException_WhenHostTestIsNotAssignableFromHostTest()
        {
            var invalidHostTest = new InvalidHostTest<HostFixture>(new HostFixture());
            Assert.Throws<ArgumentOutOfRangeException>(() => _hostFixture.ConfigureHost(invalidHostTest));
        }

        [Fact]
        public void ConfigureHost_ShouldConfigureHostSuccessfully()
        {
            var validHostTest = new ValidHostTest(_hostFixture);

            _hostFixture.ConfigureHost(validHostTest);

            Assert.NotNull(_hostFixture.Host);
            Assert.NotNull(_hostFixture.ServiceProvider);
            Assert.NotNull(_hostFixture.Configuration);
            Assert.NotNull(_hostFixture.HostingEnvironment);
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
