using System.Collections.Generic;
using System.Threading.Tasks;
using AppspaceTechChallenge.API.Models.Billboards;

namespace AppspaceTechChallenge.API.Contracts
{
    public interface IBillboardService
    {
        public Task<IEnumerable<BillboardDTO>> BuildIntelligentBillboards(IntelligentBillboardRequest intelligentBillboardRequest);
    }
}