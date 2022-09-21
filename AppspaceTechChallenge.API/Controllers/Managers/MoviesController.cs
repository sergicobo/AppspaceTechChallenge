using AppspaceTechChallenge.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Controllers.Managers
{
    [Route("managers/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUpcomingRecommendations([FromQuery] int timePeriodFromNow, [FromQuery] List<string> ageRates, [FromQuery] List<string> genres)
        {
            return NotFound("... Coming Soon ...");
        }
    }
}
