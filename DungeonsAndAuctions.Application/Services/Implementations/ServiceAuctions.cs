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

        public async Task<List<AuctionsDTO>> GetAuctionsByObjectID(int id)
        {
            var auctions = await _repository.GetAuctionsByObjectID(id);
            return _mapper.Map<List<AuctionsDTO>>(auctions);
        }
        public async Task<List<AuctionsDTO>> GetSpecificViewList()
        {
            var auctions = await _repository.GetSpecificViewList();
            return _mapper.Map<List<AuctionsDTO>>(auctions);
        }

        public async Task<List<AuctionsDTO?>> GetAllAuctionsBanned()
        {
            var auctions = await _repository.GetAllAuctionsBanned();
            return _mapper.Map<List<AuctionsDTO?>>(auctions);
        }

        public async Task<List<AuctionsDTO?>> GetAllAuctionsClosed()
        {

            var auctions = await _repository.GetAllAuctionsClosed();
            return _mapper.Map<List<AuctionsDTO?>>(auctions);
        }

        public async Task<List<AuctionsDTO>> GetAllAuctions()
        {
            var auctions = await _repository.GetAllAuctions();
            return _mapper.Map<List<AuctionsDTO>>(auctions);

        }

        public Task CreateAuction(AuctionsDTO auction)
        {

            var auctionEntity = _mapper.Map<Auctions>(auction);
            return _repository.CreateAuction(auctionEntity);
        }



        public Task DeleteAuction(int id)
        {

            return _repository.DeleteAuction(id);
        }

        public Task<List<AuctionsDTO>> GetAuctionsBySellerID(int sellerId)
        {

            return _repository.GetAuctionsBySellerID(sellerId)
                .ContinueWith(task => _mapper.Map<List<AuctionsDTO>>(task.Result));

        }

        public async Task<AuctionsDTO?> GetAuctionById(int id)
        {
            var auction = await _repository.GetAuctionById(id);
            return _mapper.Map<AuctionsDTO?>(auction);
        }

        public async Task UpdateAuction(AuctionsDTO auct)
        {
            var auctionEntity = _mapper.Map<Auctions>(auct);
            await _repository.UpdateAuction(auctionEntity);
        }




        public Task CancellAuction(int id)
        {

            return _repository.CancellAuction(id);
        }

        public Task PublishAuction(int id)
        {

            return _repository.PublishAuction(id);
        }

        public Task BanAuction(int id)
        {

            return _repository.BanAuction(id);
        }


        public async Task<List<AuctionsDTO?>> GetAllAuctionsValid()
        {
            var auctions = await _repository.GetAllAuctionsValid();
            return _mapper.Map<List<AuctionsDTO?>>(auctions);
        }

        public Task CloseAuction(int id)
        {
            
            return _repository.CloseAuction(id);
        }
    }
}
