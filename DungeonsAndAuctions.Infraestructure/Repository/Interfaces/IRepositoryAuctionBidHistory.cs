using D_A.Infraestructure.Models;

namespace D_A.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryAuctionBidHistory
    {
        Task<ICollection<AuctionBidHistory>> ListAsync();
        Task<int> CountBidsByBuyerAsync(int userId);
        Task<int> CountBidsByAuction(int AuctionId);

       


        Task<List<AuctionBidHistory>> GetBidsByAuctionAsync(int auctionId);
        Task<AuctionBidHistory?> GetHighestBidAsync(int auctionId);
        Task AddBidAsync(AuctionBidHistory bid);
        Task MarkAllBidsAsNotLastAsync(int auctionId);
    }
}