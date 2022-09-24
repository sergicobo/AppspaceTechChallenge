using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppspaceTechChallenge.Domain.Models;

namespace AppspaceTechChallenge.Domain.Repositories
{
    public interface IBeezyCinemaRepository
    {
        Task<IEnumerable<GenreData>> GetGenres();
        Task<IEnumerable<MovieData>> GetMovies(string city, DateTime startDate, DateTime endDate,
            bool blockbuster);
    }
}