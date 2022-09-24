using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppspaceTechChallenge.Domain.Models;

namespace AppspaceTechChallenge.Domain.Proxies
{
    public interface ITMDBProxy
    {
        Task<IEnumerable<GenreData>> GetGenres();
        Task<IEnumerable<MovieData>> GetMovies(int page, DateTime startDate, DateTime endDate, bool blockbuster);
    }
}