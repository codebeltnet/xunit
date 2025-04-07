using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting.Assets
{
    public class InvalidHostTest<T> : Test, IClassFixture<T> where T : class, IGenericHostFixture
    {
        public InvalidHostTest(T hostFixture)
        {
        }
    }
}
