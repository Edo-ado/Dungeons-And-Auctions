using D_A.Infraestructure.Models;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceAuctionWinner
    {
        Task CreateWinnerAsync(int auctionId, int userId, decimal finalPrice, int bidWinningId);
        Task<AuctionWinner?> GetByAuctionAsync(int auctionId);
    }
}