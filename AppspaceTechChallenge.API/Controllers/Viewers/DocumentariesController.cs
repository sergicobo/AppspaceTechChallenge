using AppspaceTechChallenge.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Controllers.Viewers
{
    /// <inheritdoc />
    [ApiExplorerSettings(GroupName = "Viewers")]
    [Route("api/viewers/documentaries")]
    [ApiController]
    public class DocumentariesController : ControllerBase
    {
        /// <inheritdoc />
        /// 
        /// <summary>
        /// Get recommendations for documentaries filtered by topics.
        /// </summary>
        /// <remarks>
        /// All-time recommended documentaries based on topics.
        /// </remarks>
        /// <param name="topics">Topics covered by the documentary</param>
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(DocumentaryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTimeRecommendations([FromQuery] List<string> topics)
        {
            return NotFound("... Coming Soon ...");
        }
    }
}
