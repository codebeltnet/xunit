using System;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Assets;
using Microsoft.AspNetCore.Builder;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    public class ManagedWebHostFixtureTest : Test
    {
        public ManagedWebHostFixtureTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void ConfigureHost_NullHostTest_ThrowsArgumentNullException()
        {
            // Arrange
            var fixture = new ManagedWebHostFixture();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => fixture.ConfigureHost(null));
        }

        [Fact]
        public void ConfigureHost_InvalidHostTestType_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var fixture = new ManagedWebHostFixture();
            var invalidHostTest = new InvalidHostTest<ManagedWebHostFixture>(fixture);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => fixture.ConfigureHost(invalidHostTest));
        }

        [Fact]
        public void ConfigureApplicationCallback_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var fixture = new ManagedWebHostFixture();
            Action<IApplicationBuilder> callback = app => { };

            // Act
            fixture.ConfigureApplicationCallback = callback;

            // Assert
            Assert.Equal(callback, fixture.ConfigureApplicationCallback);
        }
        
        [Fact]
        public void ConfigureHost_ValidHostTest_ConfiguresHostCorrectly()
        {
            // Arrange
            var fixture = new ManagedWebHostFixture();
            var hostTest = new ValidHostTest(fixture);

            // Act
            fixture.ConfigureHost(hostTest);

            // Assert
            Assert.NotNull(fixture.Host);
            Assert.NotNull(fixture.Application);
        }
    }
}
