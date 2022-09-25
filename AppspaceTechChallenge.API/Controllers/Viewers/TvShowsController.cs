using AppspaceTechChallenge.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Controllers.Viewers
{
    /// <inheritdoc />
    [ApiExplorerSettings(GroupName = "Viewers")]
    [Route("api/viewers/tv-shows")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        /// <inheritdoc />
        /// 
        /// <summary>
        /// Get recommendations for TV shows filtered by keywords or genres.
        /// </summary>
        /// <remarks>
        /// All-time recommended TV shows based on keywords that you like, genres you prefer or a combination of both.
        /// </remarks>
        /// <param name="keywords">Keywords used to identify the TV show</param>
        /// <param name="genres">Genres which TV show belongs</param>
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(TvShowDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTimeRecommendations([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
        {
            return NotFound("... Coming Soon ...");
        }
    }
}
