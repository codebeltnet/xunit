using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Assets
{
    public class InvalidHostTest<T> : Test, IClassFixture<T> where T : class, IGenericHostFixture
    {
        public InvalidHostTest(T hostFixture)
        {
        }
    }
}
