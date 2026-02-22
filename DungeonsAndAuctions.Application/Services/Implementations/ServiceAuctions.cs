using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Infraestructure.Repository.Interfaces;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;

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

        public async Task<List<AuctionsDTO>> GetAllAuctions()
        {
            var auctions = await _repository.GetAllAuctions();
            return _mapper.Map<List<AuctionsDTO>>(auctions);
        }

        public async Task<List<AuctionsDTO>> GetSpecificViewList()
        {
            var auctions = await _repository.GetSpecificViewList();
            return _mapper.Map<List<AuctionsDTO>>(auctions);
        }




    }
}
