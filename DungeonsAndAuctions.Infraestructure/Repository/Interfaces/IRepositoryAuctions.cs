using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryAuctions
    {

        Task<List<Models.Auctions>> GetAllAuctions();
        Task<List<Models.Auctions?>> GetSpecificViewList();
    }
}
