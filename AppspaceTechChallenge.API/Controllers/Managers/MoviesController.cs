using System;
using AppspaceTechChallenge.API.Contracts;
using AppspaceTechChallenge.API.Models;
using AppspaceTechChallenge.API.Models.Billboards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppspaceTechChallenge.API.Controllers.Managers
{
    /// <inheritdoc />
    [ApiExplorerSettings(GroupName = "Managers")]
    [Route("api/managers/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IBillboardService _billboardService;
        
        /// <inheritdoc />

        public MoviesController(IBillboardService billboardService)
        {
            _billboardService = billboardService;
        }

        /// <summary>
        /// Get recommendations for upcoming movies filtered by age rating and genres.
        /// </summary>
        /// <remarks>
        /// Recommended upcoming movies (specifying a period of time from now) based on age rate and genre.
        /// </remarks>
        /// <param name="timePeriodFromNow">Period of time from now</param>
        /// <param name="ageRates">Age rating qualification of the movie</param>
        /// <param name="genres">Genres which movie belongs</param>
        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUpcomingRecommendations([BindRequired][FromQuery] int timePeriodFromNow,
            [BindRequired][FromQuery] List<string> ageRates,
            [BindRequired][FromQuery] List<string> genres)
        {
            return NotFound("... Coming Soon ...");
        }

        /// <summary>
        /// Get recommendations for suggested movies filtered by time period and number of screens.
        /// </summary>
        /// <remarks>
        /// Build a suggested billboard for your theatre for a specific period of one or more weeks. 
        /// </remarks>
        /// <param name="timePeriod">Period of time from now</param>
        /// <param name="numberOfScreens">Age rating qualification of the movie</param>
        [HttpGet("suggested")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSuggestedBillboard([FromQuery] int timePeriod, [FromQuery] int numberOfScreens)
        {
            return NotFound("... Coming Soon ...");
        }

        /// <summary>
        ///  Build a suggested intelligent billboard for your theatre for a specific period.
        /// </summary>
        /// <remarks>
        /// <b> If a city are provided, the billboard will be generated based on the filters provided and which are also similar to other movies that have been successful in the city. </b><br/>
        /// <b> If the city are not existing in our databases, an empty billboard will be provided. </b><br/>
        /// <b> If no city provided, the billboard will be generated bases directly on the filters provided. </b><br/>
        /// </remarks>
        /// <param name="request">Pass the class BillboardDTO</param>
        [HttpGet("intelligent")]
        [ProducesResponseType(typeof(List<BillboardDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetSuggestedIntelligentBillboard([FromQuery] IntelligentBillboardRequest request)
        {
            try
            {
                return Ok(await _billboardService.BuildIntelligentBillboards(request));
            }
            catch (Exception ex)
            {
                return ValidationProblem(ex.Message, statusCode: StatusCodes.Status422UnprocessableEntity);
            }
        }
    }
}
