using AutoMapper;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.Services.Implementations
{
    public class ServiceAuctions : IServiceAuctions
    {

        private readonly IRepositoryAuctions _repository;
        private readonly IMapper _mapper;

        public ServiceAuctions(IRepositoryAuctions repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CountAuctionsBySellerAsync(int userId)
        {
            var contauctions = await _repository.CountAuctionsBySellerAsync(userId);
            return _mapper.Map<int>(contauctions);
        }

        public async Task<List<AuctionsDTO?>> GetAllAuctionsActive()
        {
            var auctions = await _repository.GetAllAuctionsActive();
            return _mapper.Map<List<AuctionsDTO?>>(auctions);
        }

        public async Task<List<AuctionsDTO?>> GetAllAuctionsInactive()
        {
            var auctions = await _repository.GetAllAuctionsInactive();
            return _mapper.Map<List<AuctionsDTO?>>(auctions);
        }


        public async Task<AuctionsDTO?> AllDetails(int id)
        {
            var auction = await _repository.AllDetails(id);
            return _mapper.Map<AuctionsDTO?>(auction);
        }




    }
}
