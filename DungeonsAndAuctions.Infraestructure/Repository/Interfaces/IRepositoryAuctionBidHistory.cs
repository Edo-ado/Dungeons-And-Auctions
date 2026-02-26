using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Infraestructure.Models;

namespace D_A.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryAuctionBidHistory
    {
        Task<ICollection<AuctionBidHistory>> ListAsync(); 

     
        Task<int> CountBidsByBuyerAsync(int userId); //cantidad bids del comprador (RoleId=1)

    }

}
