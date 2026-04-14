using D_A.Application.DTOs;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceAuctionBidHistory
    {
        Task<int> CountBidsByBuyerAsync(int userId);
        Task<string?> ListAsync();
        Task<int> CountBidsByAuction(int AuctionId);



      
        Task<List<AuctionBidHistoryDTO>> GetBidsByAuctionAsync(int auctionId);
        Task<AuctionBidHistoryDTO?> GetHighestBidAsync(int auctionId);
        Task<string?> PlaceBidAsync(int auctionId, int userId, decimal amount);
    }
}