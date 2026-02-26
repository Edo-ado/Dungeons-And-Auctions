using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Infraestructure.Models;

namespace D_A.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryObject
    {
        Task<ICollection<Objects>> ListAsync();
        Task<Objects?> FindByIdAsync(int id);
    }
}
