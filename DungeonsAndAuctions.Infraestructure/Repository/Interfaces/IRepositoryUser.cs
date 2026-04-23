
using D_A.Infraestructure.Models;
using DNDA.Web.Models.Reports;

namespace D_A.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryUser
    {
        // ─── Existentes ───────────────────────────────────────────────────────
        Task<ICollection<Users>> ListAsync();
        Task<Users?> FindByIdAsync(int id);
        Task<Users?> FindByIdForUpdateAsync(int id);
        Task UpdateAsync(Users entity);
        Task ToggleBlockAsync(int id);
        Task ToggleActiveAsync(int id);
        Task<Users?> GetWinnerUserByPaymentAsync(int winnerUserId);
        Task<ICollection<BuyerActivity>> GetBuyerActivityReportAsync(DateTime dateFrom, DateTime dateTo);

        // ─── Nuevos para Auth ─────────────────────────────────────────────────


        Task<Users?> FindByEmailAsync(string email);

       
        Task<Users?> LoginAsync(string email, byte[] passwordHash);

  
        Task<int> CreateAsync(Users entity);
    }
}