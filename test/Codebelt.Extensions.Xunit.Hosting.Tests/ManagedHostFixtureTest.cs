using System;
using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.Assets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting
{
    public class ManagedHostFixtureTest : Test
    {
        private readonly ManagedHostFixture _hostFixture;

        public ManagedHostFixtureTest(ITestOutputHelper output) : base(output)
        {
            _hostFixture = new ManagedHostFixture();
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
            var validHostTest = new ValidHostTest(_hostFixture);

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
