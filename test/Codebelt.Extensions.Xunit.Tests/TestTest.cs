using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

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

        public override Task InitializeAsync()
        {
            _initializeAsyncCalled = true;
            return Task.CompletedTask;
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
