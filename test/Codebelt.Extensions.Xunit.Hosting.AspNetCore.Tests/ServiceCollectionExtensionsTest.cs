using System.ComponentModel;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    public class ServiceCollectionExtensionsTest : Test
    {
        public ServiceCollectionExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData(ServiceLifetime.Transient)]
        [InlineData(ServiceLifetime.Scoped)]
        [InlineData(ServiceLifetime.Singleton)]
        public void AddFakeHttpContextAccessor_ShouldAddService(ServiceLifetime lifetime)
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddFakeHttpContextAccessor(lifetime);
            var serviceProvider = services.BuildServiceProvider();
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();

            // Assert
            Assert.NotNull(httpContextAccessor);
            Assert.IsType<FakeHttpContextAccessor>(httpContextAccessor);
        }

        [Fact]
        public void AddFakeHttpContextAccessor_ShouldThrowInvalidEnumArgumentException_ForInvalidLifetime()
        {
            // Arrange
            var services = new ServiceCollection();
            var invalidLifetime = (ServiceLifetime)999;

            // Act & Assert
            Assert.Throws<InvalidEnumArgumentException>(() => services.AddFakeHttpContextAccessor(invalidLifetime));
        }
    }
}
