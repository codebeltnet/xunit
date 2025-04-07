namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Represents the members needed for ASP.NET Core (including but not limited to MVC, Razor and related) testing.
    /// </summary>
    /// <seealso cref="IHostTest"/>
    /// <seealso cref="IPipelineTest" />
    public interface IWebHostTest : IHostTest, IPipelineTest
    {
    }
}
