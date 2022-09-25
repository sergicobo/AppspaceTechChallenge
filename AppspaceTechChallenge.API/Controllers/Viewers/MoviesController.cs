using AppspaceTechChallenge.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Controllers.Viewers
{
    /// <inheritdoc />
    [ApiExplorerSettings(GroupName = "Viewers")]
    [Route("api/viewers/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        /// <inheritdoc />
        /// 
        /// <summary>
        /// Get recommendations for movies filtered by keywords or genres.
        /// </summary>
        /// <remarks>
        /// All-time recommended movies based on keywords that you like, genres you prefer or a combination of both.
        /// </remarks>
        /// <param name="keywords">Keywords used to identify the movie</param>
        /// <param name="genres">Genres which movie belongs</param>
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTimeRecommendations([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
        {
            return NotFound("... Coming Soon ...");
        }

        /// <summary>
        /// Get recommendations for upcoming movies filtered by keywords or genres.
        /// </summary>
        /// <remarks>
        /// Recommended upcoming movies(specifying a period of time from now) based on keywords that you like, genres you prefer or a combination of both.
        /// </remarks>
        /// <param name="keywords">Keywords used to identify the movie</param>
        /// <param name="genres">Genres which movie belongs</param>
        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUpcomingRecommendations([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
        {
            return NotFound("... Coming Soon ...");
        }
    }
}
