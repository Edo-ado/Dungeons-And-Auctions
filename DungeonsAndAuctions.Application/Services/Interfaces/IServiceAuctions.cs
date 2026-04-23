using D_A.Application.DTOs;
using D_A.Infraestructure.Models;
using DNDA.Web.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceAuctions
    {
        Task<List<AuctionsDTO?>> GetAllAuctionsActive();
        Task<List<AuctionsDTO?>> GetAllAuctionsInactive();
        Task<List<AuctionsDTO?>> GetAllAuctionsBanned();
        Task<List<AuctionsDTO?>> GetAllAuctionsClosed();
        Task<List<AuctionsDTO>> GetAllAuctions();


        Task<AuctionsDTO?> AllDetails(int id);
        Task<int> CountAuctionsBySellerAsync(int userId);
        Task<List<AuctionsDTO>> GetSpecificViewList();
        Task<List<AuctionsDTO>> GetAuctionsByObjectID(int id);
        Task<AuctionsDTO?> GetAuctionById(int id);

        Task<SystemActivity> GetSystemActivityReportAsync(DateTime dateFrom, DateTime dateTo);


        Task CreateAuction(AuctionsDTO auction);
        Task UpdateAuction(AuctionsDTO auction);
        Task DeleteAuction(int id);
        Task<List<AuctionsDTO>> GetAuctionsBySellerID(int sellerId);


        Task CancellAuction(int id);
        Task PublishAuction(int id);
        Task BanAuction(int id);
        Task CloseAuction(int id);
        Task<List<AuctionsDTO?>> GetAllAuctionsValid();

    }
}
