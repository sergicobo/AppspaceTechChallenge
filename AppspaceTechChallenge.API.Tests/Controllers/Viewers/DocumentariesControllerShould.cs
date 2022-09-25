using System.Collections.Generic;
using AppspaceTechChallenge.API.Controllers.Viewers;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AppspaceTechChallenge.API.Tests.Controllers.Viewers
{
	public class DocumentariesControllerShould
	{
		private readonly NotFoundObjectResult _notFoundResult;

		public DocumentariesControllerShould()
		{
			_notFoundResult = new NotFoundObjectResult("... Coming Soon ...");
		}

		[Theory, AutoData]
		public void Return_NotFoundError_When_Ask_For_All_Time_Recommendations(List<string> topics)
		{
			var controller = new DocumentariesController();

			var result = controller.GetAllTimeRecommendations(topics);

			result.Should().NotBeNull();
			((NotFoundObjectResult) result).StatusCode.Should().Be(_notFoundResult.StatusCode);
			((NotFoundObjectResult) result).Value.Should().Be(_notFoundResult.Value);
		}
	}
}
