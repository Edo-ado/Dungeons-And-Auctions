using System.Collections.Generic;
using System.Threading.Tasks;
using D_A.Application.DTOs;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceAuctionBidHistory
    {
        Task<int> CountBidsByBuyerAsync(int userId);
        Task<string?> ListAsync();
    }
}
