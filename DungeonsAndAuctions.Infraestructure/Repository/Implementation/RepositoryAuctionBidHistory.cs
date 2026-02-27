using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<ICollection<AuctionBidHistory>> ListAsync()
        {
            return await _context.Set<AuctionBidHistory>()
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
