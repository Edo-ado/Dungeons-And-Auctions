

using D_A.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryAuctions
    {
        Task<List<Auctions?>> GetAllAuctionsInactive();
        Task<List<Auctions?>> GetAllAuctionsActive();
        Task<Auctions?> AllDetails(int id);
        Task<int> CountAuctionsBySellerAsync(int userId);//cantidad subastas de vendedor (RoleId=2)
        Task<List<Auctions>> GetSpecificViewList();
        Task<List<Auctions>> GetAuctionsByObjectID(int id);

    }
}