
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

  
        Task<Users?> GetProfileAsync(int userId);
        Task<bool> UpdateProfileAsync(int userId, string firstName, string lastName, string? phoneNumber, string? aboutMe);
        Task<bool> ChangePasswordAsync(int userId, byte[] currentPasswordHash, byte[] newPasswordHash);
        Task<ICollection<AuctionBidHistory>> GetBidHistoryAsync(int userId);
        Task<ICollection<Auctions>> GetUserAuctionsAsync(int userId);
        Task<ICollection<Payment>> GetUserPaymentsAsync(int userId);



        Task<Users?> FindByEmailAsync(string email);

       
        Task<Users?> LoginAsync(string email, byte[] passwordHash);

  
        Task<int> CreateAsync(Users entity);
    }
}