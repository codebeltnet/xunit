using Microsoft.AspNetCore.Mvc;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Assets
{
    [ApiController]
    [Route("[controller]")]
    public class FakeController : ControllerBase
    {
        public FakeController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Unit Test");
        }
    }
}
