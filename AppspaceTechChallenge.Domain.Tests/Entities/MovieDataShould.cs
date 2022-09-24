using AppspaceTechChallenge.Domain.Entities;
using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using Xunit;

namespace AppspaceTechChallenge.Domain.Tests.Entities
{
    public class MovieDataShould
    {
        [Theory, AutoData]
        public void Get_Movies_With_Expected_Data(string title, string overview, 
            int[] genres, string language, DateTime releaseDate, bool blockbuster)
        {
            var movie = new MovieData(title, overview, genres, language, releaseDate, blockbuster);

            movie.GetTitle().Should().Be(title);
            movie.GetOverview().Should().Be(overview);
            movie.GetGenres().Should().NotBeEmpty()
                .And.HaveCount(genres.Length)
                .And.ContainInOrder(genres)
                .And.ContainItemsAssignableTo<int>();
            movie.GetLanguage().Should().Be(language);
            movie.GetReleaseDate().Should().Be(releaseDate);
            movie.GetBlockbuster().Should().Be(blockbuster);
        }
    }
}
