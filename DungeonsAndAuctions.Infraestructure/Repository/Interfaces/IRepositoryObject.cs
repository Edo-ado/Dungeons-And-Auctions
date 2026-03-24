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
        Task<ICollection<Objects>> ListActiveAsync();

        Task<Objects?> FindByIdAsync(int id);
        Task<List<Categories>> GetCategoriesByIdObject(int id);

        //mantenimientos
        Task<int> AddAsync(Objects entity, List<int> selectedCategorias, List<byte[]> imagenes);
        Task UpdateAsync(Objects entity, List<int> selectedCategorias, List<byte[]> imagenes);
        Task DeleteAsync(int id);
        Task ToggleActiveAsync(int id);
        Task<bool> HasActiveAuctionAsync(int objectId);
        Task<bool> HasAuctionPárticipationAsync(int objectId);

        Task<bool> HasBeenAuctionedAsync(int objectId);




    }
}
