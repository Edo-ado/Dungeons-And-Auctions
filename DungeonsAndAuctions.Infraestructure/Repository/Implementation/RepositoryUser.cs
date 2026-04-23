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
    }
}