using Microsoft.AspNetCore.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

internal sealed class DeferredInvalidWebApplicationTest : WebApplicationTest<ManagedWebHostFixture, ManagedWebApplicationFixture<ManagedWebHostFixture>>
{
    public DeferredInvalidWebApplicationTest(ManagedWebApplicationFixture<ManagedWebHostFixture> hostFixture) : base(true, hostFixture)
    {
    }

    public void Configure(IWebHostBuilder builder)
    {
    }
}
