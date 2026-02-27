using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Infraestructure.Models;

namespace D_A.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryCountry 
    {
        Task<ICollection<Countries>> ListAsync();
        Task<Countries?> GetCountryById(int id);
    }
}
