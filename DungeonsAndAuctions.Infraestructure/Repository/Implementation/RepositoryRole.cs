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
    public class RepositoryRole : IRepositoryRole

    {
        private readonly DAContext _context;

        public RepositoryRole(DAContext context)
        {
            _context = context;
        }

        public async Task<Roles?> GetRolById(int id)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }


        public async Task<ICollection<Roles>> ListAsync()
        {
            return await _context.Roles
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
