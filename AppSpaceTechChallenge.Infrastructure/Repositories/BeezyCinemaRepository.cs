using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AppspaceTechChallenge.Domain.Entities;
using AppspaceTechChallenge.Domain.Repositories;
using AppspaceTechChallenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AppspaceTechChallenge.Infrastructure.Repositories
{
    public class BeezyCinemaRepository : IBeezyCinemaRepository
    {
        private readonly BeezyCinemaContext _beezyCinemaContext;

        public BeezyCinemaRepository(BeezyCinemaContext beezyCinemaContext)
        {
            _beezyCinemaContext = beezyCinemaContext;
        }


        public async Task<IEnumerable<GenreData>> GetGenres()
        {
            return await _beezyCinemaContext.Genre.Select(g => new GenreData(g.Id, g.Name)).ToListAsync();
        }

        public async Task<IEnumerable<MovieData>> GetMovies(string city, DateTime startDate, DateTime endDate, bool blockbuster)
        {
            var cityCapitalized = Capitalize(city);

            var cityFromDb = await _beezyCinemaContext.City
                .Where(c => c.Name == cityCapitalized)
                .Include(c => c.Cinema)
                .ThenInclude(c => c.Room)
                .FirstOrDefaultAsync();

            if (cityFromDb == null) return new List<MovieData>();

            var cityRooms = cityFromDb.Cinema.Select(c => c.Room).SelectMany(r => r).Distinct();

            var movieRooms = await _beezyCinemaContext.Movie
                .Include(m => m.Session)
                .ThenInclude(s => s.Room)
                .ToListAsync();

            var moviesOnCity = movieRooms
                .SelectMany(mr => mr.Session.Select(s => new {movie = mr, room = s.Room}))
                .Where(movie => cityRooms.ToList().Contains(movie.room)).Distinct();

            var movieGenres = await _beezyCinemaContext.MovieGenre.ToListAsync();

            var movies = moviesOnCity.SelectMany(m => 
                m.movie.Session.Select(s => new {movie = s.Movie, room = s.Room, seats = s.SeatsSold, size = s.Room.Size}))
            .Where(mr => cityRooms.Contains(mr.room) && mr.size == GetPreferedSize(blockbuster))
            .GroupBy(ms => new {ms.movie, ms.size}, ms => ms.seats ?? 0,
                (mz, seats) => new {movieSeats = mz, seatsSold = seats.Sum()})
            .OrderByDescending(m => m.seatsSold)
            .ThenByDescending(m => m.movieSeats.movie.ReleaseDate)
            .Where(m => m.movieSeats.movie.ReleaseDate >= startDate && m.movieSeats.movie.ReleaseDate <= endDate)
            .Select(movie => new MovieData
            {
                Title = movie.movieSeats.movie.OriginalTitle,
                Overview = string.Empty,
                Language = movie.movieSeats.movie.OriginalLanguage,
                Genres = movieGenres.Where(mg => mg.MovieId == movie.movieSeats.movie.Id).Select(mg => mg.GenreId).ToArray(),
                ReleaseDate = movie.movieSeats.movie.ReleaseDate,
                Blockbuster = movie.movieSeats.size == "Big"
            });

            return movies;
        }

        private string GetPreferedSize(bool blockbuster)
        {
            return blockbuster ? "Big" : "Small";
        }

        private string Capitalize(string city)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(city);
        }
    }
}
