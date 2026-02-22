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
    public class RepositoryUser : IRepositoryUser

    {
        private readonly DAContext _context;

        public RepositoryUser(DAContext context)
        {
            _context = context;
        }

        public async Task<Users?> FindByIdAsync(int id)
        {
            return await _context.Set<Users>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ICollection<Users>> ListAsync()
        {
            //Select * from Autor
            var collection = await _context.Set<Users>()
            .AsNoTracking()
            .ToListAsync();
            return collection;
        }


    }
}
