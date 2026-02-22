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

        public async Task<Auctions> GetSoftViewList()
        {
            var collection = await _context.Set<Auctions>()
                .AsNoTracking()
                .Include(Name => Name.IdobjectNavigation)
                .Include(Images => Images.id)

        }




    }
}
