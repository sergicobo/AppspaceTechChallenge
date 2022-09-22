using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppspaceTechChallenge.API.Contracts;
using AppspaceTechChallenge.API.Models;
using AppspaceTechChallenge.Domain.Proxies;

namespace AppspaceTechChallenge.API.Services
{
    public class BillboardService : IBillboardService
    {
        private readonly ITMDBProxy _tmdbProxy;

        public BillboardService(ITMDBProxy tmdbProxy)
        {
            _tmdbProxy = tmdbProxy;
        }

        public async Task<List<MovieDTO>> BuildIntelligentBillboards()
        {
            var proxyMovies = await _tmdbProxy.GetMovies();

            return proxyMovies.Select(m => new MovieDTO()
            {
                Title = m.Title,
                Language = m.Language,
                Overview = m.Overview,
                Genres = m.Genres,
                ReleaseDate = m.ReleaseDate
            }).ToList();
        }

        public void BuildSuggestedBillboards()
        {
            throw new System.NotImplementedException();
        }
    }
}
