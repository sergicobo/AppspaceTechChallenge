using Microsoft.AspNetCore.Mvc;

namespace AppspaceTechChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        public TestController()
        {
        }

        [HttpGet]
        public OkObjectResult Get()
        {
            return Ok("This is a test");
        }
    }
}
