using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Infraestructure.Data;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace D_A.Infraestructure.Repository.Implementation
{
    public class RepositoryCategory : IRepositoryCategory
    {

        private readonly DAContext _context;

        public RepositoryCategory(DAContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Categories>> ListAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
