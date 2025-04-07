using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.Internal
{
    internal sealed class HostTest : HostTest<IGenericHostFixture>
    {
        private readonly Action<IServiceCollection> _serviceConfigurator;
        private readonly Action<HostBuilderContext, IServiceCollection> _serviceConfiguratorWithContext;
        private readonly Action<IHostBuilder> _hostConfigurator;
        private HostBuilderContext _hostBuilderContext;

        internal HostTest(Action<IServiceCollection> serviceConfigurator, Action<IHostBuilder> hostConfigurator, IGenericHostFixture hostFixture) : base(true, hostFixture, callerType: serviceConfigurator?.Target?.GetType() ?? hostConfigurator?.Target?.GetType())
        {
            _serviceConfigurator = serviceConfigurator;
            _hostConfigurator = hostConfigurator;
            InitializeHostFixture(hostFixture);
        }

        internal HostTest(Action<HostBuilderContext, IServiceCollection> serviceConfigurator, Action<IHostBuilder> hostConfigurator, IGenericHostFixture hostFixture) : base(true, hostFixture, callerType: serviceConfigurator?.Target?.GetType() ?? hostConfigurator?.Target?.GetType())
        {
            _serviceConfiguratorWithContext = serviceConfigurator;
            _hostConfigurator = hostConfigurator;
            InitializeHostFixture(hostFixture);
        }

        private void InitializeHostFixture(IGenericHostFixture hostFixture)
        {
            if (!hostFixture.HasValidState())
            {
                hostFixture.ConfigureHostCallback = ConfigureHost;
                hostFixture.ConfigureCallback = Configure;
                hostFixture.ConfigureServicesCallback = ConfigureServices;
                hostFixture.ConfigureHost(this);
            }
            Host = hostFixture.Host;
            Configure(hostFixture.Configuration, hostFixture.Environment);
        }

        protected override void ConfigureHost(IHostBuilder hb)
        {
            _hostBuilderContext = new HostBuilderContext(hb.Properties);
            _hostConfigurator?.Invoke(hb);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            _serviceConfigurator?.Invoke(services);
            _serviceConfiguratorWithContext?.Invoke(Tweaker.Adjust(_hostBuilderContext, hbc =>
            {
                hbc.Configuration = Configuration;
                hbc.HostingEnvironment = Environment;
                return hbc;
            }), services);
        }
    }
}
