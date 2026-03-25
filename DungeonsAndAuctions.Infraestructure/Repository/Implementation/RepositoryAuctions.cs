using D_A.Infraestructure.Data;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Infraestructure.Repository.Implementation
{
    public class RepositoryAuctions : IRepositoryAuctions
    {

        private readonly DAContext _context;


        public RepositoryAuctions(DAContext context)
        {
            _context = context;
        }

        public async Task<Auctions?> AllDetails(int id)
        {
            var detail = await _context.Auctions
            .AsNoTracking()
            .Where(a => a.Id == id)

            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.Category)

            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdImageNavigation)


            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdQualityNavigation)

            .Include(a => a.AuctionBidHistory)

            .Include(a => a.AuctionBidHistory)
            .ThenInclude(b => b.User)

            .Include(u => u.IdusercreatorNavigation)


            .Include(a => a.IdobjectNavigation)

            .Include(u => u.IdusercreatorNavigation)
            .Include(s => s.IdstateNavigation)


            .FirstOrDefaultAsync();

            return detail;
        }

        public async Task<List<Auctions?>> GetAllAuctionsActive()
        {
            var detail = await _context.Auctions
            .AsNoTracking()
            .Where(a => a.Idstate == 1)

            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdImageNavigation)

            .Include(a => a.AuctionBidHistory)
            .Include(a => a.IdobjectNavigation)


            .Include(u => u.IdusercreatorNavigation)
            .Include(s => s.IdstateNavigation)
            .ToListAsync();

            return detail;


        }
        public async Task<List<Auctions?>> GetAllAuctionsValid()
        {
            var detail = await _context.Auctions
            .AsNoTracking()
            .Where(a => a.Idstate != 6)

            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdImageNavigation)

            .Include(a => a.AuctionBidHistory)
            .Include(a => a.IdobjectNavigation)


            .Include(u => u.IdusercreatorNavigation)
            .Include(s => s.IdstateNavigation)
            .ToListAsync();

            return detail;


        }

        public async Task<List<Auctions?>> GetAllAuctionsInactive()
        {
            var detail = await _context.Auctions
            .AsNoTracking()
            .Where(a => a.Idstate == 4)
            .Include(a => a.AuctionBidHistory)
            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdImageNavigation)
            .Include(u => u.IdusercreatorNavigation)
            .Include(s => s.IdstateNavigation)
            .ToListAsync();

            return detail;


        }

        public async Task<List<Auctions?>> GetAllAuctionsBanned()
        {
            var detail = await _context.Auctions
            .AsNoTracking()
            .Where(a => a.Idstate == 3)
            .Include(a => a.AuctionBidHistory)
            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdImageNavigation)
            .Include(u => u.IdusercreatorNavigation)
            .Include(s => s.IdstateNavigation)
            .ToListAsync();

            return detail;


        }

        public async Task<List<Auctions?>> GetAllAuctionsClosed()
        {
            var detail = await _context.Auctions
            .AsNoTracking()
            .Where(a => a.Idstate == 2)
            .Include(a => a.AuctionBidHistory)
            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdImageNavigation)
            .Include(u => u.IdusercreatorNavigation)
            .Include(s => s.IdstateNavigation)
            .ToListAsync();

            return detail;


        }

        public async Task<int> CountAuctionsBySellerAsync(int userId)
        {
            return await _context.Auctions
            .CountAsync(a => a.Idusercreator == userId && _context.Users.Any(u => u.Id == userId && u.RoleId == 2));
        }

        public async Task<List<Auctions>> GetAuctionsByObjectID(int id)
        {
            return await _context.Auctions
            .AsNoTracking()
            .Where(a => a.Idobject == id)
              .Include(a => a.IdstateNavigation)
            .ToListAsync();
        }

        public async Task<List<Auctions>> GetSpecificViewList()
        {
            return await _context.Auctions
            .AsNoTracking()
            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdImageNavigation)
            .Include(u => u.IdusercreatorNavigation)
            .Include(s => s.IdstateNavigation)
            .ToListAsync();
        }


        public async Task<List<Auctions>> GetAllAuctions()
        {
            var detail = await _context.Auctions
            .AsNoTracking()
            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdImageNavigation)

            .Include(a => a.AuctionBidHistory)
            .Include(u => u.IdusercreatorNavigation)
            .Include(s => s.IdstateNavigation)
            .ToListAsync();

            return detail;
        }




        public async Task CreateAuction(Auctions auction)
        {

            if (auction != null)
            {

                _context.Auctions.Add(auction);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException(nameof(auction), "The auction cannot be null.");
            }

        }

        public async Task UpdateAuction(Auctions auction)
        {

            if( auction.Idstate != 1) {


                _context.Auctions.Update(auction);

                _context.Entry(auction).Property(u => u.Idusercreator).IsModified = false;
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new InvalidOperationException("Cannot edit an active auction.");

            }
    
        }

        public async Task DeleteAuction(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction != null)
            {
                auction.IsActive = !auction.IsActive;
                await _context.SaveChangesAsync();
            }
           
         
          

        }

        public async Task<List<Auctions>> GetAuctionsBySellerID(int sellerId)
        {
            return await _context.Auctions
            .AsNoTracking()
            .Where(a => a.Idusercreator == sellerId)
            .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdImageNavigation)
            .Include(u => u.IdusercreatorNavigation)
            .Include(s => s.IdstateNavigation)
            .ToListAsync();
        }

        public async Task<Auctions?> GetAuctionById(int id)
        {
            return await _context.Auctions
                .AsNoTracking()
                .Include(a => a.IdstateNavigation)
                .Include(a => a.IdobjectNavigation)
              
                    .ThenInclude(o => o.IdImageNavigation)
                      .Include(a => a.AuctionBidHistory)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    







        public async Task CancellAuction(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction != null)
            {
                auction.Idstate = 4; // Assuming 4 represents "Cancelled"
                await _context.SaveChangesAsync();
            }
        }

        public async Task PublishAuction(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction != null)
            {
                auction.Idstate = 1; // Assuming 1 represents "Active"
                await _context.SaveChangesAsync();
            }
        }

        public async Task BanAuction(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction != null)
            {
                auction.Idstate = 3; // Assuming 1 represents "Active"
                await _context.SaveChangesAsync();
            }
        }
        }
}