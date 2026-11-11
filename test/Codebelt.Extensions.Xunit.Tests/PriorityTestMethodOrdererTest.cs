using Xunit;

namespace Codebelt.Extensions.Xunit;

[TestMethodOrderer(typeof(PriorityTestMethodOrderer))]
public class PriorityTestMethodOrdererTest : Test
{
    private static int _sequence;

    public PriorityTestMethodOrdererTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact, Priority(0)]
    public void ShouldRunSecond_WhenPriorityIsZero()
    {
        Assert.Equal(1, _sequence++);
    }

    [Fact, Priority(-1)]
    public void ShouldRunFirst_WhenPriorityIsNegative()
    {
        Assert.Equal(0, _sequence++);
    }

    [Fact, Priority(1)]
    public void AShouldRunThird_WhenPriorityIsOne()
    {
        Assert.Equal(2, _sequence++);
    }

    [Fact, Priority(1)]
    public void BShouldRunFourth_WhenPriorityIsOne()
    {
        Assert.Equal(3, _sequence++);
    }

    [Fact]
    public void ShouldRunLast_WhenPriorityIsMissing()
    {
        Assert.Equal(4, _sequence++);
    }
}
