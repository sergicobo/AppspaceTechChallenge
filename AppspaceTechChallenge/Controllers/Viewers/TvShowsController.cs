﻿using AppspaceTechChallenge.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AppspaceTechChallenge.API.Controllers.Viewers
{
    [Route("[controller]")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(TvShow), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllTimeRecommendations([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
        {
            return NotFound("... Coming Soon ...");
        }
    }
}
