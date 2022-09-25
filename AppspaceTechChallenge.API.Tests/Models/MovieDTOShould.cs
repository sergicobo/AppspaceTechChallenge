using AppspaceTechChallenge.API.Models;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace AppspaceTechChallenge.API.Tests.Models
{
    public class MovieDTOShould
    {
        [Theory, AutoData]
        public void Get_MovieDTO_With_Expected_Data(string title, string overview, IEnumerable<string> genres, string language,
            DateTime releaseDate, string webSite, IEnumerable<string> associatedKeywords)
        {
            var movieDto = new MovieDTO(title, overview, genres, language, releaseDate, webSite, associatedKeywords);

            movieDto.Title.Should().Be(title);
            movieDto.Overview.Should().Be(overview);
            movieDto.Genres.Should().BeEquivalentTo(genres);
            movieDto.Language.Should().Be(language);
            movieDto.ReleaseDate.Should().Be(releaseDate);
            movieDto.WebSite.Should().Be(webSite);
            movieDto.AssociatedKeywords.Should().BeEquivalentTo(associatedKeywords);
        }
    }
}
