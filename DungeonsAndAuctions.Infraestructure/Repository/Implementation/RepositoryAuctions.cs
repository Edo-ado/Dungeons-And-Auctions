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
                .ThenInclude(o => o.IdimageNavigation)

                 .Include(a => a.IdobjectNavigation)
                .ThenInclude(o => o.IdQualityNavigation)


                .Include(a => a.AuctionBidHistory)
               .ThenInclude(b => b.User)

                .Include(u => u.IdusercreatorNavigation)


                .Include(a => a.IdobjectNavigation)
         .ThenInclude(o => o.IdimageNavigation)

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
         .ThenInclude(o => o.IdimageNavigation)

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
    .Where(a => a.Idstate != 1)

    .Include(a => a.IdobjectNavigation)
        .ThenInclude(o => o.IdimageNavigation)
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




    }
}