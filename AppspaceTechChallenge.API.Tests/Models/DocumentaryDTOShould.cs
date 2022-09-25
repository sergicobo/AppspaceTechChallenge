using AppspaceTechChallenge.API.Models;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace AppspaceTechChallenge.API.Tests.Models
{
    public class DocumentaryDTOShould
    {
        [Theory, AutoData]
        public void Get_DocumentaryDTO_With_Expected_Data(string title, string overview, IEnumerable<string> genres, string language,
            DateTime releaseDate, string webSite, IEnumerable<string> associatedKeywords)
        {
            var documentaryDto = new DocumentaryDTO(title, overview, genres, language, releaseDate, webSite, associatedKeywords);

            documentaryDto.Title.Should().Be(title);
            documentaryDto.Overview.Should().Be(overview);
            documentaryDto.Genres.Should().BeEquivalentTo(genres);
            documentaryDto.Language.Should().Be(language);
            documentaryDto.ReleaseDate.Should().Be(releaseDate);
            documentaryDto.WebSite.Should().Be(webSite);
            documentaryDto.AssociatedKeywords.Should().BeEquivalentTo(associatedKeywords);
        }
    }
}
