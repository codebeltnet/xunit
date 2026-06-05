using Microsoft.AspNetCore.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

internal sealed class DeferredInvalidWebApplicationTest : WebApplicationTest<ManagedWebHostFixture, BlockingManagedWebApplicationFixture<ManagedWebHostFixture>>
{
    public DeferredInvalidWebApplicationTest(BlockingManagedWebApplicationFixture<ManagedWebHostFixture> hostFixture) : base(true, hostFixture)
    {
    }

    public void Configure(IWebHostBuilder builder)
    {
    }
}
