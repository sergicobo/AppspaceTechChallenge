using AppspaceTechChallenge.API.Models;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using AppspaceTechChallenge.API.Models.Billboards;
using FluentAssertions;
using Xunit;

namespace AppspaceTechChallenge.API.Tests.Models.Billboards
{
	public class BillboardDTOShould
	{
		[Theory, AutoData]
		public void Get_Billboards_With_Expected_Data(DateTime startDate, IEnumerable<MovieDTO> bigScreenMovies, IEnumerable<MovieDTO> smallScreenMovies)
		{
			var billboard = new BillboardDTO(startDate, bigScreenMovies, smallScreenMovies);

			billboard.StartDate.Should().Be(startDate);
			billboard.BigScreenMovies.Should().BeEquivalentTo(bigScreenMovies);
			billboard.SmallScreenMovies.Should().BeEquivalentTo(smallScreenMovies);
		}
	}
}
