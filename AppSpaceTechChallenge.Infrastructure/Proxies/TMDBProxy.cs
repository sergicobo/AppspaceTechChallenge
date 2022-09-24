using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppspaceTechChallenge.Domain.Entities;
using AppspaceTechChallenge.Domain.Proxies;
using AppspaceTechChallenge.Infrastructure.Models.Proxy;
using Tiny.RestClient;

namespace AppspaceTechChallenge.Infrastructure.Proxies
{
    public class TMDBProxy : ITMDBProxy
    {
        private readonly TinyRestClient _client;

        public TMDBProxy()
        {
            _client = new TinyRestClient(new HttpClient(), "https://api.themoviedb.org/3");
        }

        public async Task<IEnumerable<GenreData>> GetGenres()
        {
            var genres = await _client.GetRequest("/genre/movie/list")
                .AddQueryParameter("api_key", "424bbceb07c8558b2658f2ca533c915b")
                .ExecuteAsync<TMDBGenres>();

            return genres.Result.Select(g => new GenreData(g.Id, g.Name));
        }

        public async Task<IEnumerable<MovieData>> GetMovies(int page, DateTime startDate, DateTime endDate, bool blockbuster)
        {
            var movies = await RequestMovies(startDate, endDate, page, blockbuster);
            return movies;
        }

        private async Task<IEnumerable<MovieData>> RequestMovies(DateTime startDate, DateTime endDate, int page, bool blockbuster)
        {
            var moviesFromProxy = await _client.GetRequest("discover/movie")
                .AddQueryParameter("api_key", "424bbceb07c8558b2658f2ca533c915b")
                .AddQueryParameter("release_date.gte", startDate.ToString("yyyy-MM-dd"))
                .AddQueryParameter("release_date.lte", endDate.ToString("yyyy-MM-dd"))
                .AddQueryParameter("sort_by", blockbuster ? "popularity.desc" : "popularity.asc")
                .AddQueryParameter("page", page)
                .ExecuteAsync<TMDBResult>();

            return moviesFromProxy.Results.Select(m => new MovieData
            {
                Title = m.OriginalTitle,
                Language = m.OriginalLanguage,
                Overview = m.Overview,
                Genres = m.Genres,
                ReleaseDate = m.ReleaseDate,
                Blockbuster = blockbuster
            });
        }
    }
}
