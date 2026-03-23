using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs.Api;

namespace D_A.Application.Services.Interfaces.Api
{
    public interface IUserApiQueryService
    {

        Task<ICollection<UserApiDto>> ListAsync();
        Task<UserApiDto?> FindByIdAsync(int id);

    }
}
