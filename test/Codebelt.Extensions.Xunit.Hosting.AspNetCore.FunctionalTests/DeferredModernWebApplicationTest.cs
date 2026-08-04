using Microsoft.AspNetCore.Hosting;
using ModernProgram = Codebelt.Extensions.Xunit.Hosting.Program.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

internal sealed class DeferredModernWebApplicationTest : WebApplicationTest<ModernProgram, ManagedWebApplicationFixture<ModernProgram>>
{
    public DeferredModernWebApplicationTest(ManagedWebApplicationFixture<ModernProgram> hostFixture) : base(true, hostFixture)
    {
    }

    public void Configure(IWebHostBuilder builder)
    {
    }
}
