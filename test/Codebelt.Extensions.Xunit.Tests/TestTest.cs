using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.v3;

namespace Codebelt.Extensions.Xunit
{
    public class TestTest : Test
    {
        private const string ExpectedStringValue = "AllIsGood";
        private bool _onDisposeManagedResourcesCalled;
        private bool _initializeAsyncCalled;

        public TestTest(ITestOutputHelper output) : base(output)
        {
        }

        public override ValueTask InitializeAsync()
        {
            _initializeAsyncCalled = true;
            return default;
        }

        protected override ValueTask OnDisposeManagedResourcesAsync()
        {
            TestOutput.WriteLine($"{nameof(IAsyncLifetime.DisposeAsync)} was called.");
            return default;
        }

        [Fact]
        public void Test_InitializeAsyncCalled_ShouldBeTrue()
        {
            Assert.True(_initializeAsyncCalled);
        }

        [Fact]
        public void Test_ShouldHaveTestOutput()
        {
            Assert.True(HasTestOutput);
            Assert.IsAssignableFrom<ITestOutputHelper>(TestOutput);
            Assert.IsType<TestOutputHelper>(TestOutput);
        }

        [Fact]
        public void Test_ShouldInvokeDispose()
        {
            Assert.Equal(ExpectedStringValue, DisposeSensitiveMethod());
            Assert.False(_onDisposeManagedResourcesCalled);
            Dispose();
            Assert.True(_onDisposeManagedResourcesCalled);
            Assert.True(Disposed);
            Assert.Throws<ObjectDisposedException>(DisposeSensitiveMethod);
        }

        [Fact]
        public void Test_ShouldHaveCallerTypeOfTestTest()
        {
            Assert.True(GetType() == typeof(TestTest), "GetType() == typeof(TestTest)");
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenExactMatch()
        {
            var expected = "Hello World";
            var actual = "Hello World";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnFalse_WhenNoMatch()
        {
            var expected = "Hello World";
            var actual = "Goodbye World";

            var result = Test.Match(expected, actual);

            Assert.False(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenUsingGroupOfCharactersWildcard()
        {
            var expected = "Hello * World";
            var actual = "Hello Beautiful World";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenUsingSingleCharacterWildcard()
        {
            var expected = "Hello ?orld";
            var actual = "Hello World";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenUsingMultipleWildcards()
        {
            var expected = "* test ? result *";
            var actual = "This is a test 1 result with more text";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenUsingGroupOfCharactersWildcardAtStart()
        {
            var expected = "*World";
            var actual = "Hello World";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenUsingGroupOfCharactersWildcardAtEnd()
        {
            var expected = "Hello*";
            var actual = "Hello World";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnFalse_WhenWildcardPatternDoesNotMatch()
        {
            var expected = "Hello ? World";
            var actual = "Hello Beautiful World";

            var result = Test.Match(expected, actual);

            Assert.False(result);
        }

        [Fact]
        public void Match_ShouldThrowArgumentOutOfRangeException_WhenThrowOnNoMatchIsTrue()
        {
            var expected = "Hello World";
            var actual = "Goodbye World";

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                Test.Match(expected, actual, options => options.ThrowOnNoMatch = true));

            TestOutput.WriteLine(exception.Message);
            TestOutput.WriteLine($"ParamName: {exception.ParamName}");
            TestOutput.WriteLine($"ActualValue: {exception.ActualValue}");
            TestOutput.WriteLine($"Expected: {expected}");

            Assert.Equal("expected", exception.ParamName);
            Assert.Contains("The expected value does not match the actual value", exception.Message);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WithCustomGroupOfCharacters()
        {
            var expected = "Hello ## World";
            var actual = "Hello Beautiful World";

            var result = Test.Match(expected, actual, options => options.GroupOfCharacters = "\\#\\#");

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WithCustomSingleCharacter()
        {
            var expected = "Hello #orld";
            var actual = "Hello World";

            var result = Test.Match(expected, actual, options => options.SingleCharacter = "\\#");

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenMatchingMultilineStrings()
        {
            var expected = $"Line 1{Environment.NewLine}Line 2{Environment.NewLine}Line 3";
            var actual = $"Line 1{Environment.NewLine}Line 2{Environment.NewLine}Line 3";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenMatchingMultilineStringsWithWildcards()
        {
            var expected = $"Line 1{Environment.NewLine}Line ?{Environment.NewLine}Line *";
            var actual = $"Line 1{Environment.NewLine}Line 2{Environment.NewLine}Line 3";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnFalse_WhenMultilineStringsDoNotMatch()
        {
            var expected = $"Line 1{Environment.NewLine}Line 2{Environment.NewLine}Line 3";
            var actual = $"Line 1{Environment.NewLine}Line 4{Environment.NewLine}Line 3";

            var result = Test.Match(expected, actual);

            Assert.False(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenMatchingEmptyStrings()
        {
            var expected = string.Empty;
            var actual = string.Empty;

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenMatchingSpecialRegexCharacters()
        {
            var expected = "Hello.World";
            var actual = "Hello.World";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenMatchingWithParentheses()
        {
            var expected = "Test (value)";
            var actual = "Test (value)";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenMatchingWithSquareBrackets()
        {
            var expected = "Test [value]";
            var actual = "Test [value]";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenMatchingWithPlusSign()
        {
            var expected = "Test+Value";
            var actual = "Test+Value";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldReturnFalse_WhenSingleCharacterWildcardMatchesMultipleCharacters()
        {
            var expected = "Test?Value";
            var actual = "TestMultipleValue";

            var result = Test.Match(expected, actual);

            Assert.False(result);
        }

        [Fact]
        public void Match_ShouldReturnTrue_WhenGroupOfCharactersWildcardMatchesZeroCharacters()
        {
            var expected = "Test*Value";
            var actual = "TestValue";

            var result = Test.Match(expected, actual);

            Assert.True(result);
        }

        [Fact]
        public void Match_ShouldThrowArgumentOutOfRangeException_WithNonMatchingLines()
        {
            var expected = $"Line 1{Environment.NewLine}Expected Line{Environment.NewLine}Line 3";
            var actual = $"Line 1{Environment.NewLine}Actual Line{Environment.NewLine}Line 3";

            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                Test.Match(expected, actual, options => options.ThrowOnNoMatch = true));

            Assert.Contains("Expected Line", exception.ActualValue.ToString());
        }

        public string DisposeSensitiveMethod()
        {
            if (Disposed) { throw new ObjectDisposedException(GetType().FullName); }
            return ExpectedStringValue;
        }

        protected override void OnDisposeManagedResources()
        {
            _onDisposeManagedResourcesCalled = true;
            base.OnDisposeManagedResources();
        }
    }
}
