// Path: DungeonsAndAuctions.Application/Services/Interfaces/IServiceUser.cs
using D_A.Application.DTOs;
using DNDA.Web.Models.Reports;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceUser
    {
        // ─── Existentes ───────────────────────────────────────────────────────
        Task<List<UsersDTO>> ListAsync();
        Task<UsersDTO> FindByIdAsync(int id);
        Task UpdateAsync(int id, UsersDTO dto);
        Task<ICollection<BuyerActivity>> GetBuyerActivityReportAsync(DateTime dateFrom, DateTime dateTo);
        Task ToggleBlockAsync(int id);
        Task ToggleActiveAsync(int id);
        Task<UsersDTO> GetWinnerUserByPaymentAsync(int winnerUserId);

        
        Task<UsersDTO?> LoginAsync(string email, string password);

      
        Task<UsersDTO?> RegisterAsync(RegisterUserDTO dto);

      
        Task<bool> EmailExistsAsync(string email);
    }
}