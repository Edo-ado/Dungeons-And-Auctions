using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;
using DNDA.Web.Models.Reports;

namespace D_A.Application.Services.Interfaces
{
    public interface IServiceUser
    {

        Task<List<UsersDTO>> ListAsync();

        Task<UsersDTO> FindByIdAsync(int id);

        Task UpdateAsync(int id, UsersDTO dto);

        Task<ICollection<BuyerActivity>> GetBuyerActivityReportAsync(DateTime dateFrom, DateTime dateTo);
        Task ToggleBlockAsync(int id);

        Task<UsersDTO> GetWinnerUserByPaymentAsync(int winnerUserId);
        Task ToggleActiveAsync(int id);

    }

}