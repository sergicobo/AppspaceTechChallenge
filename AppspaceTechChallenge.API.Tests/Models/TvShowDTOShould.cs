using AppspaceTechChallenge.API.Models;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace AppspaceTechChallenge.API.Tests.Models
{
    public class TvShowDTOShould
    {
        [Theory, AutoData]
        public void Get_TvShowDTO_With_Expected_Data(string title, string overview, IEnumerable<string> genres, string language,
            DateTime releaseDate, string webSite, IEnumerable<string> associatedKeywords, int numberOfSeasons, int numberOfEpisodes, bool isFinished)
        {
            var movieDto = new TvShowDTO(title, overview, genres, language, releaseDate, webSite, associatedKeywords, numberOfSeasons, numberOfEpisodes, isFinished);

            movieDto.Title.Should().Be(title);
            movieDto.Overview.Should().Be(overview);
            movieDto.Genres.Should().BeEquivalentTo(genres);
            movieDto.Language.Should().Be(language);
            movieDto.ReleaseDate.Should().Be(releaseDate);
            movieDto.WebSite.Should().Be(webSite);
            movieDto.AssociatedKeywords.Should().BeEquivalentTo(associatedKeywords);
            movieDto.NumberOfSeasons.Should().Be(numberOfSeasons);
            movieDto.NumberOfEpisodes.Should().Be(numberOfEpisodes);
            movieDto.IsFinished.Should().Be(isFinished);
        }
    }
}
