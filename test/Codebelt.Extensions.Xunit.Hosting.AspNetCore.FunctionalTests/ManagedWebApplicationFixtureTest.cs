using System;
using Xunit;
using ModernProgram = Codebelt.Extensions.Xunit.Hosting.Program.App.Program;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class ManagedWebApplicationFixtureTest : Test
{
    public ManagedWebApplicationFixtureTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void ConfigureHost_ShouldThrowArgumentOutOfRangeException_WhenHostTestIsNotWebApplicationTest()
    {
        var fixture = new ManagedWebApplicationFixture<ModernProgram>();

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => fixture.ConfigureHost(this));

        Assert.Equal("hostTest", ex.ParamName);
    }

    [Fact]
    public void ConfigureHost_ShouldThrowInvalidOperationException_WhenEntryPointAssemblyHasNoHost()
    {
        var fixture = new ManagedWebApplicationFixture<ManagedWebHostFixture>();
        var test = new DeferredInvalidWebApplicationTest(fixture);

        fixture.ConfigureCallback = test.Configure;
        fixture.ConfigureWebHostCallback = test.Configure;

        Assert.Throws<InvalidOperationException>(() => fixture.ConfigureHost(test));
    }

    [Fact]
    public void HasValidState_ShouldReturnFalse_WhenFixtureIsUninitialized()
    {
        var fixture = new ManagedWebApplicationFixture<ModernProgram>();

        Assert.False(fixture.HasValidState());
    }
}
