using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.Assets
{
    public class ValidHostTest : HostTest<HostFixture>
    {
        public ValidHostTest(HostFixture hostFixture) : base(hostFixture)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}
