using System;
using System.Collections.Generic;
using System.Linq;
using D_A.Infraestructure.Models;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceUser
    {

      Task<List<UserDTO>> ListAsync();

        Task<UserDTO> FindByIdAsync(int id);


    }

}
