using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;

namespace D_A.Application.Services.Implementations
{
    public class ServiceAuctionWinner : IServiceAuctionWinner
    {
        private readonly IRepositoryAuctionWinner _repo;

        public ServiceAuctionWinner(IRepositoryAuctionWinner repo)
        {
            _repo = repo;
        }

        public async Task CreateWinnerAsync(int auctionId, int userId, decimal finalPrice, int bidWinningId)
        {
            
            var existing = await _repo.GetByAuctionAsync(auctionId);
            if (existing != null) return;

            var winner = new AuctionWinner
            {
                Actionid = auctionId,
                Winnerid = userId,
                Finalprice = finalPrice,
                Closeddate = DateTime.Now,
                Bidwinningid = bidWinningId
            };

            await _repo.CreateAsync(winner);
        }

        public async Task<AuctionWinner?> GetByAuctionAsync(int auctionId)
        {
            return await _repo.GetByAuctionAsync(auctionId);
        }
    }
}