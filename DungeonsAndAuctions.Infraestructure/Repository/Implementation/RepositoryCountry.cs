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
    public class RepositoryCountry : IRepositoryCountry
    {

        private readonly DAContext _context;

        public RepositoryCountry(DAContext context)
        {
            _context = context;
        }
        public async Task<Countries?> GetCountryById(int id)
        {
            return await _context.Countries
       .AsNoTracking()
       .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ICollection<Countries>> ListAsync()
        {
            return await _context.Countries
                    .AsNoTracking()
                    .ToListAsync();
        }
    }
}
