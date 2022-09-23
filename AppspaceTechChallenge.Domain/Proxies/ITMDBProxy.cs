using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppspaceTechChallenge.Domain.Models;

namespace AppspaceTechChallenge.Domain.Proxies
{
    public interface ITMDBProxy
    {
        Task<IEnumerable<MovieData>> GetMovies(int totalMoviesNeeded, DateTime startDate, DateTime endDate, bool blockbuster);
        Task<IEnumerable<GenreData>> GetGenres();
    }
}