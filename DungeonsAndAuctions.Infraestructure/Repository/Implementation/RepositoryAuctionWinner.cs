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
    public class RepositoryAuctionWinner : IRepositoryAuctionWinner
    {
        private readonly DAContext _context;
        public RepositoryAuctionWinner(DAContext context) => _context = context;

        public async Task CreateAsync(AuctionWinner winner)
        {
            _context.AuctionWinner.Add(winner);
            await _context.SaveChangesAsync();
        }

        public async Task<AuctionWinner?> GetByAuctionAsync(int auctionId)
        {
            return await _context.AuctionWinner
                .FirstOrDefaultAsync(w => w.Actionid == auctionId);
        }
    }
}
