using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceObject
    {
        Task<ICollection<ObjectsDTO>> ListAsync();
        Task<ICollection<ObjectsDTO>> ListActiveAsync();

        Task<ObjectsDTO> GetObjectById(int id);

        Task<Categories?> GetCategoriesByIdObject(int id);

        //mantenimientos
        Task<int> AddAsync(ObjectsDTO entity, List<int> selectedCategorias, List<byte[]> imagenes);
        Task UpdateAsync(int id, ObjectsDTO entity, List<int> selectedCategorias, List<byte[]> imagenes);
        Task DeleteAsync(int id);
        Task ToggleActiveAsync(int id);
        Task<bool> HasActiveAuctionAsync(int objectId);
        Task<bool> HasBeenAuctionedAsync(int objectId);



    }
}
