using D_A.Application.DTOs;
using D_A.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceAuctions
    {

        Task<List<AuctionsDTO?>> GetAllAuctionsActive();
        Task<List<AuctionsDTO?>> GetAllAuctionsInactive();
        Task<AuctionsDTO?> AllDetails(int id);
        Task<int> CountAuctionsBySellerAsync(int userId);
      




    }
}
