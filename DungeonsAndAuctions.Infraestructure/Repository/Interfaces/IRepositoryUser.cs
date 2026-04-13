using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Infraestructure.Models;


namespace D_A.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryUser
    {

        Task<ICollection<Users>> ListAsync();
        Task<Users?> FindByIdAsync(int id);
        Task UpdateAsync(Users entity);
        Task<Users?> FindByIdForUpdateAsync(int id);
        Task ToggleBlockAsync(int id);
        Task ToggleActiveAsync(int id);

        Task<Users?> GetWinnerUserByPaymentAsync(int winnerUserId);
    }



}