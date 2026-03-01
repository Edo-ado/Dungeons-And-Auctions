using D_A.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Infraestructure.Repository.Interfaces
{
    internal interface IRepositoryQuaility
    {


        Task<ICollection<Qualities>> ListAsync();

        Task<Qualities> GetqualityById(int id);


    }
}
