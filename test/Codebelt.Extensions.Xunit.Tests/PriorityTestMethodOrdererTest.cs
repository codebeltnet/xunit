using System;
using System.Linq;
using Xunit;
using Xunit.v3;

namespace Codebelt.Extensions.Xunit;

public class PriorityTestMethodOrdererTest : Test
{
    public PriorityTestMethodOrdererTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void ShouldOrderMethodsByPriorityThenName_WhenPrioritiesAreMixed()
    {
        var type = typeof(MethodsToOrder);
        var assembly = new XunitTestAssembly(type.Assembly, null);
        var collection = new XunitTestCollection(assembly, null, false, "Methods to order");
        var testClass = new XunitTestClass(type, collection);
        var methods = new[]
        {
            nameof(MethodsToOrder.Unmarked),
            nameof(MethodsToOrder.ZMaximum),
            nameof(MethodsToOrder.BPositive),
            nameof(MethodsToOrder.APositive),
            nameof(MethodsToOrder.Zero),
            nameof(MethodsToOrder.Negative)
        }.Select(name => new XunitTestMethod(testClass, type.GetMethod(name), Array.Empty<object>())).ToArray();

        var ordered = new PriorityTestMethodOrderer().OrderTestMethods(methods);

        Assert.Equal(new[]
        {
            nameof(MethodsToOrder.Negative),
            nameof(MethodsToOrder.Zero),
            nameof(MethodsToOrder.APositive),
            nameof(MethodsToOrder.BPositive),
            nameof(MethodsToOrder.ZMaximum),
            nameof(MethodsToOrder.Unmarked)
        }, ordered.Select(method => method.MethodName));
    }

    private class MethodsToOrder
    {
        [Priority(-1)]
        public void Negative() { }

        [Priority(0)]
        public void Zero() { }

        [Priority(1)]
        public void APositive() { }

        [Priority(1)]
        public void BPositive() { }

        [Priority(int.MaxValue)]
        public void ZMaximum() { }

        public void Unmarked() { }
    }
}
