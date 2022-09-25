using System.Collections.Generic;
using AppspaceTechChallenge.API.Controllers.Viewers;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AppspaceTechChallenge.API.Tests.Controllers.Viewers
{
	public class MoviesControllerShould
	{
		private readonly NotFoundObjectResult _notFoundResult;

		public MoviesControllerShould()
		{
			_notFoundResult = new NotFoundObjectResult("... Coming Soon ...");
		}

		[Theory, AutoData]
		public void Return_NotFoundError_When_Ask_For_All_Time_Recommendations(List<string> keywords, List<string> genres)
		{
			var controller = new MoviesController();

			var result = controller.GetAllTimeRecommendations(keywords, genres);

			result.Should().NotBeNull();
			((NotFoundObjectResult) result).StatusCode.Should().Be(_notFoundResult.StatusCode);
			((NotFoundObjectResult) result).Value.Should().Be(_notFoundResult.Value);
		}

		[Theory, AutoData]
		public void Return_NotFoundError_When_Ask_For_Upcoming_Recommendations(List<string> keywords, List<string> genres)
		{
			var controller = new MoviesController();

			var result = controller.GetUpcomingRecommendations(keywords, genres);

			result.Should().NotBeNull();
			((NotFoundObjectResult)result).StatusCode.Should().Be(_notFoundResult.StatusCode);
			((NotFoundObjectResult)result).Value.Should().Be(_notFoundResult.Value);
		}
	}
}
