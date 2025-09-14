using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Codebelt.Extensions.Xunit.Hosting.Assets
{
    public class ValidHostTest : HostTest<ManagedHostFixture>
    {
        public ValidHostTest(ManagedHostFixture hostFixture) : base(hostFixture)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}
