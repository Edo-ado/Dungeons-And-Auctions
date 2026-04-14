using D_A.Infraestructure.Data;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace D_A.Infraestructure.Repository.Implementation
{
    public class RepositoryAuctionBidHistory : IRepositoryAuctionBidHistory
    {
        private readonly DAContext _context;

        public RepositoryAuctionBidHistory(DAContext context)
        {
            _context = context;
        }

        public async Task<int> CountBidsByBuyerAsync(int userId)
        {
            return await _context.AuctionBidHistory
                .CountAsync(bid => bid.UserId == userId &&
                    _context.Users.Any(u => u.Id == bid.UserId && u.RoleId == 1));
        }

        public async Task<List<AuctionBidHistory>> GetAllAuctionsByIdOBject(int IDOBject)
        {
            return await _context.AuctionBidHistory
                .Include(bid => bid.Auction)
                .Where(bid => bid.Auction.Idobject == IDOBject)
                .ToListAsync();
        }

        public async Task<ICollection<AuctionBidHistory>> ListAsync()
        {
            return await _context.Set<AuctionBidHistory>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> CountBidsByAuction(int AuctionId)
        {
            return await _context.AuctionBidHistory
                .CountAsync(bid => bid.AuctionId == AuctionId);
        }

       



        public async Task<List<AuctionBidHistory>> GetBidsByAuctionAsync(int auctionId)
        {
            return await _context.AuctionBidHistory
                .AsNoTracking()
                .Include(b => b.User)
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.BidDate)
                .ToListAsync();
        }

        public async Task<AuctionBidHistory?> GetHighestBidAsync(int auctionId)
        {
            return await _context.AuctionBidHistory
                .AsNoTracking()
                .Include(b => b.User)
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.Amount)
                .FirstOrDefaultAsync();
        }

        public async Task AddBidAsync(AuctionBidHistory bid)
        {
            _context.AuctionBidHistory.Add(bid);
            await _context.SaveChangesAsync();
        }

    
        public async Task MarkAllBidsAsNotLastAsync(int auctionId)
        {
            var bids = await _context.AuctionBidHistory
                .Where(b => b.AuctionId == auctionId && b.IsLastBid)
                .ToListAsync();

            foreach (var b in bids)
                b.IsLastBid = false;

            await _context.SaveChangesAsync();
        }
    }
}