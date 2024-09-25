using System.Runtime.InteropServices;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit
{
    public class StringExtensionsTest : Test
    {
        public StringExtensionsTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void ReplaceLineEndings_ShouldReplaceNewLineOccurrences()
        {
            var lineEndings = "Windows has \r\n (CRLF) and Linux has \n (LF)";

            TestOutput.WriteLine($$"""
                                   Before: {{lineEndings}}
                                   After: {{lineEndings.ReplaceLineEndings()}}
                                   """);

            TestOutput.WriteLine(RuntimeInformation.OSDescription);
            TestOutput.WriteLine(RuntimeInformation.FrameworkDescription);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Assert.Equal("Windows has \n (CRLF) and Linux has \n (LF)", lineEndings.ReplaceLineEndings());
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.Equal("Windows has \r\n (CRLF) and Linux has \r\n (LF)", lineEndings.ReplaceLineEndings());
            }
        }
    }
}
