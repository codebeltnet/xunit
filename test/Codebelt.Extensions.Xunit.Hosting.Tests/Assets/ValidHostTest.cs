using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
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
