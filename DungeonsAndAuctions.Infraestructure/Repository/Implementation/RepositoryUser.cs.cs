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
    public class RepositoryUser : IRepositoryUsers

    {
        private readonly DAContext _context;

        public RepositoryUser(DAContext context)
        {
            _context = context;
        }

        public Task<Users> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
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
