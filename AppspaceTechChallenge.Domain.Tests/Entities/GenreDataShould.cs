using AppspaceTechChallenge.Domain.Entities;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AppspaceTechChallenge.Domain.Tests.Entities
{
    public class GenreDataShould
    {
        [Theory, AutoData]
        public void Get_Genres_With_Expected_Data(int id, string name)
        {
            var genre = new GenreData(id, name);

            genre.GetId().Should().Be(id);
            genre.GetName().Should().Be(name);
        }
    }
}
