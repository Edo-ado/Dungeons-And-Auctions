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
    internal class RepositoryQuality : IRepositoryQuaility
    {

        private readonly DAContext _context;


        public RepositoryQuality(DAContext context)
        {
            _context = context;
        }

        public async Task<Qualities?> GetqualityById(int id)
        {
            
            return await _context.Qualities.FindAsync(id);



        }

        public async Task<ICollection<Qualities>> ListAsync()
        {
            return await _context.Qualities
                    .AsNoTracking()
                    .ToListAsync();


        }

    }
}
