using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Assets
{
    public class ValidHostTest : WebHostTest<ManagedWebHostFixture>
    {
        public ValidHostTest(ManagedWebHostFixture hostFixture) : base(hostFixture)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            
        }

        public override void ConfigureApplication(IApplicationBuilder app)
        {
            
        }
    }
}
