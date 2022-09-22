using System.Collections.Generic;
using System.Threading.Tasks;
using AppspaceTechChallenge.Domain.Models;

namespace AppspaceTechChallenge.Domain.Proxies
{
    public interface ITMDBProxy
    {
        Task<List<MovieData>> GetMovies();
    }
}