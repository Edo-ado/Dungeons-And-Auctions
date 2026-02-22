using D_A.Infraestructure.Data;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Infraestructure.Repository.Implementation
{
    public class RepositoryAuctions : IRepositoryAuctions
    {

        private readonly DAContext _context;


        public RepositoryAuctions(DAContext context)
        {
            _context = context;
        }

        public async Task<List<Auctions?>> GetSpecificViewList()
        {

        
            var result = await _context.Auctions
                .Include(Auctions => )

            return result;
        }

        public async Task<AuctionBidHistory?> HowManyBids(int id)
        {
            int current = 0;

            string sql = string.Format("");

            System.Data.DataTable dataTable = new System.Data.DataTable();

            System.Data.Common.DbConnection connection = _context.Database.GetDbConnection();
            System.Data.Common.DbProviderFactory dbFactory = System.Data.Common.DbProviderFactories.GetFactory(connection!)!;
            using (var cmd = dbFactory!.CreateCommand())
            {
                cmd!.Connection = connection;
                cmd.CommandText = sql;
                using (System.Data.Common.DbDataAdapter adapter = dbFactory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }


            current = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            return await Task.FromResult(current);
        }


        public async Task<Images?> GetImageById(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Auctions>> GetAllAuctions()
        {
            return await _context.Auctions.ToListAsync();

        }


    }
}
