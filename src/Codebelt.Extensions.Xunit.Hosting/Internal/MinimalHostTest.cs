using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.Internal
{
    internal sealed class MinimalHostTest : MinimalHostTest<IMinimalHostFixture>
    {
        private readonly Action<IServiceCollection> _serviceConfigurator;
        private readonly Action<HostBuilderContext, IServiceCollection> _serviceConfiguratorWithContext;
        private readonly Action<IHostApplicationBuilder> _hostConfigurator;
        private HostBuilderContext _hostBuilderContext;

        internal MinimalHostTest(Action<IServiceCollection> serviceConfigurator, Action<IHostApplicationBuilder> hostConfigurator, IMinimalHostFixture hostFixture) : base(true, hostFixture, callerType: serviceConfigurator?.Target?.GetType() ?? hostConfigurator?.Target?.GetType())
        {
            _serviceConfigurator = serviceConfigurator;
            _hostConfigurator = hostConfigurator;
            InitializeHostFixture(hostFixture);
        }

        internal MinimalHostTest(Action<HostBuilderContext, IServiceCollection> serviceConfigurator, Action<IHostApplicationBuilder> hostConfigurator, IMinimalHostFixture hostFixture) : base(true, hostFixture, callerType: serviceConfigurator?.Target?.GetType() ?? hostConfigurator?.Target?.GetType())
        {
            _serviceConfiguratorWithContext = serviceConfigurator;
            _hostConfigurator = hostConfigurator;
            InitializeHostFixture(hostFixture);
        }

        private void InitializeHostFixture(IMinimalHostFixture hostFixture)
        {
            if (!hostFixture.HasValidState())
            {
                hostFixture.ConfigureCallback = Configure;
                hostFixture.ConfigureHostCallback = ConfigureHost;
                hostFixture.ConfigureHost(this);
            }
            Host = hostFixture.Host;
            Configure(hostFixture.Configuration, hostFixture.Environment);
        }

        protected override void ConfigureHost(IHostApplicationBuilder hb)
        {
            _hostBuilderContext = new HostBuilderContext(hb.Properties);
            _hostConfigurator?.Invoke(hb);
            _serviceConfigurator?.Invoke(hb.Services);
            _serviceConfiguratorWithContext?.Invoke(Tweaker.Adjust(_hostBuilderContext, hbc =>
            {
                hbc.Configuration = hb.Configuration;
                hbc.HostingEnvironment = hb.Environment;
                return hbc;
            }), hb.Services);
        }
    }
}
