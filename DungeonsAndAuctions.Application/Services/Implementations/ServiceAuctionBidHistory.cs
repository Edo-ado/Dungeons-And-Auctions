using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Repository.Interfaces;

namespace D_A.Application.Services.Implementations
{
    public class ServiceAuctionBidHistory : IServiceAuctionBidHistory
    {
        private readonly IRepositoryAuctionBidHistory _repository;
        private readonly IMapper _mapper;

        public ServiceAuctionBidHistory(IRepositoryAuctionBidHistory repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> CountBidsByBuyerAsync(int userId)
        {
            return _repository.CountBidsByBuyerAsync(userId);
        }

        public Task<string?> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
