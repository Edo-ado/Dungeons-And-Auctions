

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
        Task<List<Auctions?>> GetAllAuctionsActive();
        Task<List<Auctions?>> GetAllAuctionsInactive();
        Task<List<Auctions?>> GetAllAuctionsBanned();
        Task<List<Auctions?>> GetAllAuctionsClosed();

        Task<List<Auctions>> GetAllAuctions();



        Task<Auctions?> AllDetails(int id);
        Task<int> CountAuctionsBySellerAsync(int userId);//cantidad subastas de vendedor (RoleId=2)
        Task<List<Auctions>> GetSpecificViewList();
        Task<List<Auctions>> GetAuctionsByObjectID(int id);


        Task<Auctions?> GetAuctionById(int id);

        Task CreateAuction(Auctions auction);

        Task UpdateAuction(Auctions auction);

        Task DeleteAuction(int id);

        Task<List<Auctions>> GetAuctionsBySellerID(int sellerId);




        Task CancellAuction(int id);
        Task PublishAuction(int id);
        Task BanAuction(int id);

        Task<List<Auctions?>> GetAllAuctionsValid();

    }
}