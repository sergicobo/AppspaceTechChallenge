using System;
using AppspaceTechChallenge.API.Contracts;
using AppspaceTechChallenge.API.Models;
using AppspaceTechChallenge.API.Models.Billboards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppspaceTechChallenge.API.Controllers.Managers
{
    [ApiExplorerSettings(GroupName = "Managers")]
    [Route("managers/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IBillboardService _billboardService;

        public MoviesController(IBillboardService billboardService)
        {
            _billboardService = billboardService;
        }

        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUpcomingRecommendations([FromQuery] int timePeriodFromNow, [FromQuery] List<string> ageRates, [FromQuery] List<string> genres)
        {
            return NotFound("... Coming Soon ...");
        }

        [HttpGet("suggested")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSuggestedBillboard([FromQuery] int timePeriod, [FromQuery] int numberOfScreens)
        {
            return NotFound("... Coming Soon ...");
        }

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
