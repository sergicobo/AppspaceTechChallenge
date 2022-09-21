using AppspaceTechChallenge.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Controllers.Viewers
{
    [ApiExplorerSettings(GroupName = "Viewers")]
    [Route("viewers/[controller]")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(TvShowDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTimeRecommendations([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
        {
            return NotFound("... Coming Soon ...");
        }
    }
}
