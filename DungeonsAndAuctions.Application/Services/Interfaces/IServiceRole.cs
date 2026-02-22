using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceRole
    {
        Task<ICollection<RoleDTO>> ListAsync();
    }
}
