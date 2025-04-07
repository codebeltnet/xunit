using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Internal
{
    internal sealed class MinimalWebHostTest : MinimalWebHostTest<IMinimalWebHostFixture>
    {
        private readonly Action<IApplicationBuilder> _pipelineConfigurator;
        private readonly Action<IServiceCollection> _serviceConfigurator;
        private readonly Action<HostBuilderContext, IApplicationBuilder> _pipelineConfiguratorWithContext;
        private readonly Action<HostBuilderContext, IServiceCollection> _serviceConfiguratorWithContext;
        private readonly Action<IHostApplicationBuilder> _hostConfigurator;
        private HostBuilderContext _hostBuilderContext;

        internal MinimalWebHostTest(Action<IServiceCollection> serviceConfigurator, Action<IApplicationBuilder> pipelineConfigurator, Action<IHostApplicationBuilder> hostConfigurator, IMinimalWebHostFixture hostFixture) : base(true, hostFixture, callerType: pipelineConfigurator?.Target?.GetType() ?? serviceConfigurator?.Target?.GetType() ?? hostConfigurator?.Target?.GetType())
        {
            _serviceConfigurator = serviceConfigurator;
            _pipelineConfigurator = pipelineConfigurator;
            _hostConfigurator = hostConfigurator;
            InitializeHostFixture(hostFixture);
        }

        internal MinimalWebHostTest(Action<HostBuilderContext, IServiceCollection> serviceConfigurator, Action<HostBuilderContext, IApplicationBuilder> pipelineConfigurator, Action<IHostApplicationBuilder> hostConfigurator, IMinimalWebHostFixture hostFixture) : base(true, hostFixture, callerType: pipelineConfigurator?.Target?.GetType() ?? serviceConfigurator?.Target?.GetType() ?? hostConfigurator?.Target?.GetType())
        {
            _serviceConfiguratorWithContext = serviceConfigurator;
            _pipelineConfiguratorWithContext = pipelineConfigurator;
            _hostConfigurator = hostConfigurator;
            InitializeHostFixture(hostFixture);
        }

        private void InitializeHostFixture(IMinimalWebHostFixture hostFixture)
        {
            if (!hostFixture.HasValidState())
            {
                hostFixture.ConfigureHostCallback = ConfigureHost;
                hostFixture.ConfigureCallback = Configure;
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
            hb.Services.AddFakeHttpContextAccessor();
        }
    }
}
