using System.Threading.Tasks;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features;
using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http.Features;

public class FakeHttpResponseFeatureTest : Test
{
    public FakeHttpResponseFeatureTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void OnStarting_ShouldInvokeCallback_OnFirstCall()
    {
        var feature = new FakeHttpResponseFeature();
        var invoked = false;

        feature.OnStarting(_ => { invoked = true; return Task.CompletedTask; }, null);

        Assert.True(invoked);
        Assert.True(feature.HasStarted);
    }

    [Fact]
    public void OnStarting_ShouldNotInvokeCallback_OnSubsequentCalls()
    {
        var feature = new FakeHttpResponseFeature();
        var count = 0;

        feature.OnStarting(_ => { count++; return Task.CompletedTask; }, null);
        feature.OnStarting(_ => { count++; return Task.CompletedTask; }, null);

        Assert.Equal(1, count);
        Assert.True(feature.HasStarted);
    }

    [Fact]
    public void OnStarting_ShouldNotThrow_WhenCallbackIsNull()
    {
        var feature = new FakeHttpResponseFeature();

        var ex = Record.Exception(() => feature.OnStarting(null, null));

        Assert.Null(ex);
        Assert.True(feature.HasStarted);
    }
}
