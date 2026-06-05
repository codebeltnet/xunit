using System;
using Xunit;
using ModernProgram = Codebelt.Extensions.Xunit.Hosting.Program.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class BlockingManagedWebApplicationFixtureTest : Test
{
    public BlockingManagedWebApplicationFixtureTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void ConfigureHost_ShouldThrowArgumentOutOfRangeException_WhenHostTestIsNotWebApplicationTest()
    {
        var fixture = new BlockingManagedWebApplicationFixture<ModernProgram>();

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => fixture.ConfigureHost(this));

        Assert.Equal("hostTest", ex.ParamName);
    }

    [Fact]
    public void ConfigureHost_ShouldThrowInvalidOperationException_WhenEntryPointAssemblyHasNoHost()
    {
        var fixture = new BlockingManagedWebApplicationFixture<ManagedWebHostFixture>();
        var test = new DeferredInvalidWebApplicationTest(fixture);

        fixture.ConfigureCallback = test.Configure;
        fixture.ConfigureWebHostCallback = test.Configure;

        Assert.Throws<InvalidOperationException>(() => fixture.ConfigureHost(test));
    }

    [Fact]
    public void HasValidState_ShouldReturnFalse_WhenFixtureIsUninitialized()
    {
        var fixture = new BlockingManagedWebApplicationFixture<ModernProgram>();

        Assert.False(fixture.HasValidState());
    }
}
