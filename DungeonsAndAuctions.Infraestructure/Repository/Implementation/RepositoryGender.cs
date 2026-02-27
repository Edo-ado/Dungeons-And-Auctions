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
    public class RepositoryGender : IRepositoryGender
    {

        private readonly DAContext _context;

        public RepositoryGender(DAContext context)
        {
            _context = context;
        }
        public async Task<Genders?> GetCountryById(int id)
        {
            return await _context.Genders
           .AsNoTracking()
           .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<ICollection<Genders>> ListAsync()
        {
            return await _context.Genders
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
