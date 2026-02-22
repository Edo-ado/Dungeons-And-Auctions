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

        public async Task<List<Auctions?>> GetSpecificViewList()
        {
            var result = await _context.Auctions
        .AsNoTracking()
        .Where(a => a.IsActive)
        .Include(a => a.IdobjectNavigation)
            .ThenInclude(o => o.IdimageNavigation)
        .Include(a => a.AuctionBidHistory) 
        .ToListAsync();

            
         
            return result;
        }

        public async Task<List<Auctions>> GetAllAuctions()
        {
            return await _context.Auctions.ToListAsync();

        }


    }
}
