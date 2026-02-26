using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceObject
    {
        Task<ICollection<ObjectsDTO>> ListAsync();
        Task<ObjectsDTO> GetObjectById(int id);
    }
}
