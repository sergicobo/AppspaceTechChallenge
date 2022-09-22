using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppspaceTechChallenge.Domain.Models;
using AppspaceTechChallenge.Domain.Proxies;
using AppspaceTechChallenge.Infrastructure.Models;
using Tiny.RestClient;

namespace AppspaceTechChallenge.Infrastructure.Proxies
{
    public class TMDBProxy : ITMDBProxy
    {
        public async Task<List<MovieData>> GetMovies()
        {
            var client = new TinyRestClient(new HttpClient(), "https://api.themoviedb.org/3");

            var movies = await client.GetRequest("discover/movie")
                .AddQueryParameter("api_key", "424bbceb07c8558b2658f2ca533c915b")
                .ExecuteAsync<TMDBResult>();

            return movies.Results
                .Select(m => new MovieData()
                {
                    Title = m.OriginalTitle,
                    Language = m.OriginalLanguage,
                    Overview = m.Overview,
                    Genres = MatchGenres(m.Genres).Result,
                    ReleaseDate = m.ReleaseDate
                }).ToList();
        }

        private async Task<List<string>> MatchGenres(int[] genreIds)
        {
            var client = new TinyRestClient(new HttpClient(), "https://api.themoviedb.org/3");

            var genres = await client.GetRequest("/genre/movie/list")
                .AddQueryParameter("api_key", "424bbceb07c8558b2658f2ca533c915b")
                .ExecuteAsync<TMDBGenres>();

            return genres.Result
                .Where(g => genreIds.Contains(g.Id))
                .Select(g => g.Name).ToList();
        }
    }
}
