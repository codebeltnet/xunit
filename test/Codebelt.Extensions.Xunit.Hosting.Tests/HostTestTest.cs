using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Codebelt.Extensions.Xunit.Hosting.Assets;
using Cuemon.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace Codebelt.Extensions.Xunit.Hosting
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class HostTestTest : HostTest<HostFixture>
    {
        private readonly IServiceScope _scope;
        private readonly Func<IList<ICorrelationToken>> _correlationsFactory;
        private static readonly ConcurrentBag<ICorrelationToken> ScopedCorrelations = new();

        public HostTestTest(HostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
        {
            _scope = hostFixture.ServiceProvider.CreateScope();
            _correlationsFactory = () => _scope.ServiceProvider.GetServices<ICorrelationToken>().ToList();
        }

        [Fact, Priority(1)]
        public void Test_SingletonShouldBeSame() // simulate a request
        {
            ScopedCorrelations.Add(_correlationsFactory().Single(c => c is ScopedCorrelation));
            var c1 = _correlationsFactory().Single(c => c is SingletonCorrelation);
            var c2 = _correlationsFactory().Single(c => c is SingletonCorrelation);
            Assert.Equal(c1.CorrelationId, c2.CorrelationId);
        }

        [Fact, Priority(2)]
        public void Test_TransientShouldBeDifferent() // simulate a request
        {
            ScopedCorrelations.Add(_correlationsFactory().Single(c => c is ScopedCorrelation));
            var c1 = _correlationsFactory().Single(c => c is TransientCorrelation);
            var c2 = _correlationsFactory().Single(c => c is TransientCorrelation);
            Assert.NotEqual(c1.CorrelationId, c2.CorrelationId);
        }

        [Fact, Priority(3)]
        public void Test_ScopedShouldBeSame() // simulate a request
        {
            ScopedCorrelations.Add(_correlationsFactory().Single(c => c is ScopedCorrelation));
            var c1 = _correlationsFactory().Single(c => c is ScopedCorrelation);
            var c2 = _correlationsFactory().Single(c => c is ScopedCorrelation);
            Assert.Equal(c1.CorrelationId, c2.CorrelationId);
        }

        [Fact]
        public void Test_ShouldHaveConfigurationEntry()
        {
            Assert.Equal("xUnit", Configuration.GetSection("unitTestTool").Value);
        }

        [Fact]
        public void Test_ShouldHaveEnvironmentOfDevelopment()
        {
            Assert.Equal("Development", HostingEnvironment.EnvironmentName);
        }

        protected override void OnDisposeManagedResources()
        {
            _scope?.Dispose();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICorrelationToken, SingletonCorrelation>();
            services.AddTransient<ICorrelationToken, TransientCorrelation>();
            services.AddScoped<ICorrelationToken, ScopedCorrelation>();
        }
    }
}
