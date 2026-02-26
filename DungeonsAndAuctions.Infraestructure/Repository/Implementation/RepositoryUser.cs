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
        //Repository se encarga de tener las consultas
        private readonly DAContext _context;

        public RepositoryUser(DAContext context)
        {
            _context = context;
        }

        public async Task<Users?> FindByIdAsync(int id)
        {
            return await _context.Set<Users>()
                .AsNoTracking()
                .Include(u => u.Gender)
                .Include(u => u.Country)
                .Include(u => u.Role)//asuñonga hay q añadirlo para q los demas tmb puedan ver el atributo
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ICollection<Users>> ListAsync()
        {
            return await _context.Users
                .Include(u => u.Role)   
                .AsNoTracking()
                .ToListAsync();
        }


    }
}
