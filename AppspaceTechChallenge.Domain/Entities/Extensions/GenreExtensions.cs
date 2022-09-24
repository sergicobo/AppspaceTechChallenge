using AppspaceTechChallenge.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AppspaceTechChallenge.Domain.Extensions
{
    public static class GenreExtensions
    {
        public static string MatchById(this IEnumerable<GenreData> genres, int genreId)
        {
            return genres.FirstOrDefault(g => g.GetId() == genreId)?.GetName();
        }
    }
}
