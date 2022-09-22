using AppspaceTechChallenge.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppspaceTechChallenge.API.Contracts
{
    public interface IBillboardService
    {
        public void BuildSuggestedBillboards();
        public Task<List<MovieDTO>> BuildIntelligentBillboards();
    }
}