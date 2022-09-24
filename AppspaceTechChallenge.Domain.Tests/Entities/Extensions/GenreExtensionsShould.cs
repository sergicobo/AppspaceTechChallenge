using System.Collections.Generic;
using AppspaceTechChallenge.Domain.Entities;
using AppspaceTechChallenge.Domain.Extensions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AppspaceTechChallenge.Domain.Tests.Entities.Extensions
{
    public class GenreExtensionsShould
    {
        [Theory, AutoData]
        public void Return_Null_If_Genres_Are_Empty(int genreId)
        {
            var genresById = new List<GenreData>();
            var match = genresById.MatchById(genreId);

            match.Should().BeNull();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void Return_Null_If_Genres_Not_Contains_GenreId(int genreId)
        {
            var genresById = SomeGenres();
            var match = genresById.MatchById(genreId);

            match.Should().BeNull();
        }

        [Theory]
        [InlineData(1, "Action")]
        [InlineData(2, "Adventure")]
        [InlineData(3, "Thriller")]
        [InlineData(4, "Comedy")]
        [InlineData(5, "Drama")]
        public void Return_Genre_If_Genres_Contains_GenreId(int genreId, string expectedGenre)
        {
            var genresById = SomeGenres();
            var match = genresById.MatchById(genreId);

            match.Should().Be(expectedGenre);
        }

        private List<GenreData> SomeGenres()
        {
            return new List<GenreData>
            {
                new GenreData(1, "Action"),
                new GenreData(2, "Adventure"),
                new GenreData(3, "Thriller"),
                new GenreData(4, "Comedy"),
                new GenreData(5, "Drama")
            };
        }
    }
}
