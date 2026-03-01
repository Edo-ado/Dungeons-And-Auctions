using D_A.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryAuctions
    {

        Task<List<Auctions?>> GetSpecificViewList();

        Task<Auctions?> allDetails(int id);

        Task<List<Auctions?>> GetAllAuctions();

        Task<int> CountAuctionsBySellerAsync(int userId);//cantidad subastas de vendedor (RoleId=2)

        Task<List<Auctions?>> GetAuctionsByObjectID(int id);


    }
}
