using D_A.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.Services.Interfaces
{
    internal interface IServiceQuality
    {



        Task<ICollection<QualitiesDTO>> ListAsync();

        Task<QualitiesDTO> GetqualityById(int id);
    }
}
