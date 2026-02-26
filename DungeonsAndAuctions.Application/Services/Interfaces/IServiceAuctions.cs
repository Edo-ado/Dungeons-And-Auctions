using D_A.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceAuctions
    {

        Task<List<AuctionsDTO>> GetAllAuctions();

        Task<List<AuctionsDTO>> GetSpecificViewList();
        Task<int> CountAuctionsBySellerAsync(int userId);


    }
}
