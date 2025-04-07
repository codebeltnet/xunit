using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Codebelt.Extensions.Xunit.Hosting.Assets;
using Cuemon.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;

namespace Codebelt.Extensions.Xunit.Hosting
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class MinimalHostTestTest : MinimalHostTest<MinimalHostFixture>
    {
        private bool _isHostRunning = false;
        private readonly IServiceScope _scope;
        private readonly Func<IList<ICorrelationToken>> _correlationsFactory;
        private static readonly ConcurrentBag<ICorrelationToken> ScopedCorrelations = new();

        public MinimalHostTestTest(MinimalHostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
        {
            _scope = hostFixture.Host.Services.CreateScope();
            _correlationsFactory = () => _scope.ServiceProvider.GetServices<ICorrelationToken>().ToList();
            var lifetime = hostFixture.Host.Services.GetRequiredService<IHostApplicationLifetime>();
            lifetime.ApplicationStarted.Register(() => _isHostRunning = true);
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
        public void Test_ShouldHaveRunningHost()
        {
            Assert.True(_isHostRunning);
        }

        [Fact]
        public void Test_ShouldHaveConfigurationEntry()
        {
            Assert.Equal("xUnit", Configuration.GetSection("unitTestTool").Value);
        }

        [Fact]
        public void Test_ShouldHaveEnvironmentOfDevelopment()
        {
            Assert.Equal("Development", Environment.EnvironmentName);
        }

        [Fact]
        public void Test_VerifyAbstractions()
        {
            using var hostTest = HostTestFactory.Create();
            Assert.IsAssignableFrom<IHostTest>(hostTest);
            Assert.IsAssignableFrom<IConfigurationTest>(hostTest);
            Assert.IsAssignableFrom<IEnvironmentTest>(hostTest);
            Assert.IsAssignableFrom<ITest>(hostTest);
            Assert.IsAssignableFrom<IDisposable>(hostTest);
            Assert.IsAssignableFrom<IAsyncDisposable>(hostTest);
        }

        protected override void OnDisposeManagedResources()
        {
            _scope?.Dispose();
        }

        protected override void ConfigureHost(IHostApplicationBuilder hb)
        {
            hb.Services.AddSingleton<ICorrelationToken, SingletonCorrelation>();
            hb.Services.AddTransient<ICorrelationToken, TransientCorrelation>();
            hb.Services.AddScoped<ICorrelationToken, ScopedCorrelation>();
        }
    }
}
