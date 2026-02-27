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
    public class RepositoryObject : IRepositoryObject
    {

        //Repository se encarga de tener las consultas
        private readonly DAContext _context;

        public RepositoryObject(DAContext context)
        {
            _context = context;
        }

        public async Task<Objects?> FindByIdAsync(int id)
        {
            return await _context.Set<Objects>()
                   .AsNoTracking()      
                   .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ICollection<Objects>> ListAsync()
        {
            return await _context.Objects
               .AsNoTracking()
               .ToListAsync();
        }
    }
}
