// Path: DungeonsAndAuctions.Infraestructure/Repository/Implementation/RepositoryUser.cs
using D_A.Infraestructure.Data;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;
using DNDA.Web.Models.Reports;
using Microsoft.EntityFrameworkCore;

namespace D_A.Infraestructure.Repository.Implementation
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly DAContext _context;

        public RepositoryUser(DAContext context)
        {
            _context = context;
        }

        // ─── Existentes ───────────────────────────────────────────────────────

        public async Task<Users?> FindByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Gender)
                .Include(u => u.Country)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ICollection<Users>> ListAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Gender)
                .Include(u => u.Country)
                .Include(u => u.Auctions)
                .Include(u => u.AuctionBidHistory)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Users entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(u => u.PasswordHash).IsModified = false;
            _context.Entry(entity).Property(u => u.RoleId).IsModified = false;

            await _context.SaveChangesAsync();
        }

        public async Task<Users?> FindByIdForUpdateAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task ToggleBlockAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return;
            user.IsBlocked = !user.IsBlocked;
            await _context.SaveChangesAsync();
        }

        public async Task ToggleActiveAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return;
            user.Active = !user.Active;
            await _context.SaveChangesAsync();
        }

        public async Task<Users?> GetWinnerUserByPaymentAsync(int winnerUserId)
        {
            var winner = await _context.AuctionWinner
                .FirstOrDefaultAsync(w => w.Idauctionwinner == winnerUserId);
            if (winner == null) return null;
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == winner.Winnerid);
        }

        public async Task<ICollection<BuyerActivity>> GetBuyerActivityReportAsync(DateTime dateFrom, DateTime dateTo)
        {
            return await _context.AuctionBidHistory
                .Where(b => b.BidDate >= dateFrom && b.BidDate <= dateTo)
                .GroupBy(b => new { b.UserId, b.User.FirstName, b.User.LastName, b.User.Email })
                .Select(g => new BuyerActivity
                {
                    BuyerName = g.Key.FirstName + " " + g.Key.LastName,
                    Email = g.Key.Email,
                    AuctionsParticipated = g.Select(b => b.AuctionId).Distinct().Count(),
                    TotalBids = g.Count()
                })
                .OrderByDescending(x => x.AuctionsParticipated)
                .ToListAsync();
        }

        // ─── Nuevos para Auth ─────────────────────────────────────────────────

        /// <summary>Busca usuario por email (para validar duplicados en registro).</summary>
        public async Task<Users?> FindByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

      
        public async Task<Users?> LoginAsync(string email, byte[] passwordHash)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Country)
                .Include(u => u.Gender)
                .FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == email.ToLower() &&
                    u.PasswordHash == passwordHash &&
                    u.Active == true &&
                    u.IsBlocked == false);
        }

    
        public async Task<int> CreateAsync(Users entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }



        public async Task<Users?> GetProfileAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Country)
                .Include(u => u.Gender)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> UpdateProfileAsync(int userId, string firstName, string lastName, string? phoneNumber, string? aboutMe)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;
            user.AboutMe = aboutMe;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAsync(int userId, byte[] currentPasswordHash, byte[] newPasswordHash)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            if (!user.PasswordHash.SequenceEqual(currentPasswordHash)) return false;

            user.PasswordHash = newPasswordHash;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<AuctionBidHistory>> GetBidHistoryAsync(int userId)
        {
            return await _context.AuctionBidHistory
                .Include(b => b.Auction)
                    .ThenInclude(a => a.IdobjectNavigation)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BidDate)
                .ToListAsync();
        }

        public async Task<ICollection<Auctions>> GetUserAuctionsAsync(int userId)
        {
            return await _context.Auctions
                .Include(a => a.IdobjectNavigation)
                .Include(a => a.IdstateNavigation)
                .Where(a => a.Idusercreator == userId)
                .OrderByDescending(a => a.StartDate)
                .ToListAsync();
        }

        public async Task<ICollection<Payment>> GetUserPaymentsAsync(int userId)
        {
            return await _context.Payment
                .Include(p => p.Auction)
                    .ThenInclude(a => a!.IdobjectNavigation)
                .Include(p => p.PaymentStatus)
                .Where(p => p.WinnerUserId != null &&
                            _context.AuctionWinner
                                .Any(w => w.Idauctionwinner == p.WinnerUserId && w.Bidwinning.UserId == userId))
                .OrderByDescending(p => p.DateCreated)
                .ToListAsync();
        }



    }
}