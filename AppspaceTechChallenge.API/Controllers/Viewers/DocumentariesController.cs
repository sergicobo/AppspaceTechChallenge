using AppspaceTechChallenge.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Controllers.Viewers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentariesController : ControllerBase
    {
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(Documentary), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTimeRecommendations([FromQuery] List<string> topics)
        {
            return NotFound("... Coming Soon ...");
        }
    }
}
