// Path: DungeonsAndAuctions.Application/Services/Interfaces/IServiceUser.cs
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;
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




        Task<Users?> GetProfileAsync(int userId);

        Task<bool> UpdateProfileAsync(int userId, string firstName, string lastName, string? phoneNumber, string? aboutMe);

        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

        Task<ICollection<AuctionBidHistory>> GetBidHistoryAsync(int userId);

        Task<ICollection<Auctions>> GetUserAuctionsAsync(int userId);

        Task<ICollection<Payment>> GetUserPaymentsAsync(int userId);









    }
}