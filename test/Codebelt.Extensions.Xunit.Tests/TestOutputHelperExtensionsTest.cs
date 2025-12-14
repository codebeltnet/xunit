using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Codebelt.Extensions.Xunit
{
    public class TestOutputHelperExtensionsTest : Test
    {
        public TestOutputHelperExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void WriteLines_WithParamsObjects_ShouldWriteMultipleLines()
        {
            var helper = new FakeTestOutputHelper();
            var values = new object[] { "Line1", "Line2", "Line3" };

            helper.WriteLines(values);

            var expected = $"Line1{Environment.NewLine}Line2{Environment.NewLine}Line3";
            Assert.Equal(expected, helper.Output);
        }

        [Fact]
        public void WriteLines_WithParamsObjects_NullHelper_ShouldThrowArgumentNullException()
        {
            ITestOutputHelper helper = null;

            var exception = Assert.Throws<ArgumentNullException>(() => helper.WriteLines(new object[] { "test" }));

            Assert.Equal("helper", exception.ParamName);
        }

        [Fact]
        public void WriteLines_WithTypedArray_ShouldWriteMultipleLines()
        {
            var helper = new FakeTestOutputHelper();
            var values = new[] { 1, 2, 3 };

            helper.WriteLines(values);

            var expected = $"1{Environment.NewLine}2{Environment.NewLine}3";
            Assert.Equal(expected, helper.Output);
        }

        [Fact]
        public void WriteLines_WithTypedArray_NullHelper_ShouldThrowArgumentNullException()
        {
            ITestOutputHelper helper = null;
            var values = new[] { 1, 2, 3 };

            var exception = Assert.Throws<ArgumentNullException>(() => helper.WriteLines(values));

            Assert.Equal("helper", exception.ParamName);
        }

        [Fact]
        public void WriteLines_WithEnumerable_ShouldWriteMultipleLines()
        {
            var helper = new FakeTestOutputHelper();
            var values = new List<string> { "First", "Second", "Third" };

            helper.WriteLines(values);

            var expected = $"First{Environment.NewLine}Second{Environment.NewLine}Third";
            Assert.Equal(expected, helper.Output);
        }

        [Fact]
        public void WriteLines_WithEnumerable_NullHelper_ShouldThrowArgumentNullException()
        {
            ITestOutputHelper helper = null;
            var values = new List<string> { "test" };

            var exception = Assert.Throws<ArgumentNullException>(() => helper.WriteLines(values));

            Assert.Equal("helper", exception.ParamName);
        }

        [Fact]
        public void WriteLines_WithEmptyArray_ShouldWriteEmptyString()
        {
            var helper = new FakeTestOutputHelper();
            var values = Array.Empty<object>();

            helper.WriteLines(values);

            Assert.Equal(string.Empty, helper.Output);
        }

        [Fact]
        public void WriteLines_WithSingleObjectInArray_ShouldWriteSingleLine()
        {
            var helper = new FakeTestOutputHelper();
            var values = new object[] { "SingleLine" };

            helper.WriteLines(values);

            Assert.Equal("SingleLine", helper.Output);
        }

        [Fact]
        public void WriteLines_WithMixedTypes_ShouldConvertToString()
        {
            var helper = new FakeTestOutputHelper();
            var values = new object[] { 42, true, 3.14, "text" };

            helper.WriteLines(values);

            var expected = $"42{Environment.NewLine}True{Environment.NewLine}{3.14.ToString()}{Environment.NewLine}text";
            Assert.Equal(expected, helper.Output);
        }

        [Fact]
        public void WriteLines_WithComplexObjects_ShouldUseToString()
        {
            var helper = new FakeTestOutputHelper();
            var values = new object[]
            {
                new TestObject { Name = "Object1" },
                new TestObject { Name = "Object2" }
            };

            helper.WriteLines(values);

            var expected = $"TestObject: Object1{Environment.NewLine}TestObject: Object2";
            Assert.Equal(expected, helper.Output);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void WriteLines_WithVariousArraySizes_ShouldHandleCorrectly(int count)
        {
            var helper = new FakeTestOutputHelper();
            var values = new string[count];
            var expectedBuilder = new StringBuilder();

            for (var i = 0; i < count; i++)
            {
                values[i] = $"Item{i}";
                if (i > 0) { expectedBuilder.Append(Environment.NewLine); }
                expectedBuilder.Append($"Item{i}");
            }

            helper.WriteLines(values);

            Assert.Equal(expectedBuilder.ToString(), helper.Output);
        }


        [Fact]
        public void WriteLines_WithEnumerableOfIntegers_ShouldFormatCorrectly()
        {
            var helper = new FakeTestOutputHelper();
            var values = new List<int> { 100, 200, 300 };

            helper.WriteLines(values);

            var expected = $"100{Environment.NewLine}200{Environment.NewLine}300";
            Assert.Equal(expected, helper.Output);
        }

        [Fact]
        public void WriteLines_WithEnumerableOfBooleans_ShouldFormatCorrectly()
        {
            var helper = new FakeTestOutputHelper();
            var values = new List<bool> { true, false, true };

            helper.WriteLines(values);

            var expected = $"True{Environment.NewLine}False{Environment.NewLine}True";
            Assert.Equal(expected, helper.Output);
        }

        [Fact]
        public void WriteLines_WithEnumerableOfDecimals_ShouldFormatCorrectly()
        {
            var helper = new FakeTestOutputHelper();
            var values = new List<decimal> { 1.5m, 2.75m, 3.99m };

            helper.WriteLines(values);

            var expected = $"{1.5m.ToString()}{Environment.NewLine}{2.75m.ToString()}{Environment.NewLine}{3.99m.ToString()}";
            Assert.Equal(expected, helper.Output);
        }

        [Fact]
        public void WriteLines_WithEnumerableOfDoubles_ShouldFormatCorrectly()
        {
            var helper = new FakeTestOutputHelper();
            var values = new List<double> { 1.1, 2.2, 3.3 };

            helper.WriteLines(values);

            var expected = $"{1.1.ToString()}{Environment.NewLine}{2.2.ToString()}{Environment.NewLine}{3.3.ToString()}";
            Assert.Equal(expected, helper.Output);
        }

        [Fact]
        public void WriteLines_WithEmptyEnumerable_ShouldWriteEmptyString()
        {
            var helper = new FakeTestOutputHelper();
            var values = new List<string>();

            helper.WriteLines(values);

            Assert.Equal(string.Empty, helper.Output);
        }

        private class FakeTestOutputHelper : ITestOutputHelper
        {
            private readonly StringBuilder _output = new();

            public string Output => _output.ToString();

            public void Write(string message)
            {
                _output.Append(message);
            }

            public void Write(string format, params object[] args)
            {
                _output.Append(string.Format(format, args));
            }

            public void WriteLine(string message)
            {
                _output.Append(message);
            }

            public void WriteLine(string format, params object[] args)
            {
                _output.Append(string.Format(format, args));
            }
        }

        private class TestObject
        {
            public string Name { get; set; }

            public override string ToString()
            {
                return $"TestObject: {Name}";
            }
        }
    }
}
