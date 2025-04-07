using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Internal
{
    internal sealed class WebHostTest : WebHostTest<IWebHostFixture>
    {
        private readonly Action<IApplicationBuilder> _pipelineConfigurator;
        private readonly Action<IServiceCollection> _serviceConfigurator;
        private readonly Action<HostBuilderContext, IApplicationBuilder> _pipelineConfiguratorWithContext;
        private readonly Action<HostBuilderContext, IServiceCollection> _serviceConfiguratorWithContext;
        private readonly Action<IHostBuilder> _hostConfigurator;
        private HostBuilderContext _hostBuilderContext;

        internal WebHostTest(Action<IServiceCollection> serviceConfigurator, Action<IApplicationBuilder> pipelineConfigurator, Action<IHostBuilder> hostConfigurator, IWebHostFixture hostFixture) : base(true, hostFixture, callerType: pipelineConfigurator?.Target?.GetType() ?? serviceConfigurator?.Target?.GetType() ?? hostConfigurator?.Target?.GetType())
        {
            _serviceConfigurator = serviceConfigurator;
            _pipelineConfigurator = pipelineConfigurator;
            _hostConfigurator = hostConfigurator;
            InitializeHostFixture(hostFixture);
        }

        internal WebHostTest(Action<HostBuilderContext, IServiceCollection> serviceConfigurator, Action<HostBuilderContext, IApplicationBuilder> pipelineConfigurator, Action<IHostBuilder> hostConfigurator, IWebHostFixture hostFixture) : base(true, hostFixture, callerType: pipelineConfigurator?.Target?.GetType() ?? serviceConfigurator?.Target?.GetType() ?? hostConfigurator?.Target?.GetType())
        {
            _serviceConfiguratorWithContext = serviceConfigurator;
            _pipelineConfiguratorWithContext = pipelineConfigurator;
            _hostConfigurator = hostConfigurator;
            InitializeHostFixture(hostFixture);
        }

        private void InitializeHostFixture(IWebHostFixture hostFixture)
        {
            if (!hostFixture.HasValidState())
            {
                hostFixture.ConfigureHostCallback = ConfigureHost;
                hostFixture.ConfigureCallback = Configure;
                hostFixture.ConfigureServicesCallback = ConfigureServices;
                hostFixture.ConfigureApplicationCallback = ConfigureApplication;
                hostFixture.ConfigureHost(this);
            }
            Host = hostFixture.Host;
            Application = hostFixture.Application;
            Configure(hostFixture.Configuration, hostFixture.Environment);
        }

        public override void ConfigureApplication(IApplicationBuilder app)
        {
            _pipelineConfigurator?.Invoke(app);
            _pipelineConfiguratorWithContext?.Invoke(Tweaker.Adjust(_hostBuilderContext, hbc =>
            {
                hbc.Configuration = Configuration;
                hbc.HostingEnvironment = Environment;
                return hbc;
            }), app);
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
            services.AddFakeHttpContextAccessor();
        }
    }
}
