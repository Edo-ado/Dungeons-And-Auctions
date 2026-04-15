using AutoMapper;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;

namespace D_A.Application.Services.Implementations
{
    public class ServiceAuctionBidHistory : IServiceAuctionBidHistory
    {
        private readonly IRepositoryAuctionBidHistory _repository;
        private readonly IRepositoryAuctions _repositoryAuctions;
        private readonly IMapper _mapper;

        public ServiceAuctionBidHistory(
            IRepositoryAuctionBidHistory repository,
            IRepositoryAuctions repositoryAuctions,
            IMapper mapper)
        {
            _repository = repository;
            _repositoryAuctions = repositoryAuctions;
            _mapper = mapper;
        }

        public Task<int> CountBidsByBuyerAsync(int userId)
            => _repository.CountBidsByBuyerAsync(userId);

        public Task<string?> ListAsync()
            => throw new NotImplementedException();

        public Task<int> CountBidsByAuction(int AuctionId)
            => _repository.CountBidsByAuction(AuctionId);

        public async Task<List<AuctionBidHistoryDTO>> GetBidsByAuctionAsync(int auctionId)
        {
            var bids = await _repository.GetBidsByAuctionAsync(auctionId);
            return _mapper.Map<List<AuctionBidHistoryDTO>>(bids);
        }

        public async Task<AuctionBidHistoryDTO?> GetHighestBidAsync(int auctionId)
        {
            var bid = await _repository.GetHighestBidAsync(auctionId);
            return bid == null ? null : _mapper.Map<AuctionBidHistoryDTO>(bid);
        }



        //eta vaina me quedo grande asi que la deje para el final, pero es la mas importante, es la que registra las pujas, y tiene que validar un monton de cosas antes de registrar la puja, asi que la deje para el final, y no se si me va a dar tiempo de hacerla, pero bueno, vamos a intentarlo
        public async Task<string?> PlaceBidAsync(int auctionId, int userId, decimal amount)
        {
            // Obtiene la subasta con sus datos
            var auction = await _repositoryAuctions.GetAuctionById(auctionId);
            if (auction == null)
                return "La subasta no existe.";

            //  Valida que la subasta está activa 
            if (auction.Idstate != 1)
                return "La subasta no está activa. No se pueden registrar pujas.";

            // Validar que el usuario no es el vendedor
            if (auction.Idusercreator == userId)
                return "No puedes pujar en tu propia subasta.";

            //  Obtiene la puja más alta actual
            var highestBid = await _repository.GetHighestBidAsync(auctionId);
            decimal currentHighest = highestBid?.Amount ?? auction.BasePrice;
            decimal incremento = auction.IncrementoMinimo;

            //  Valida incremento mínimo
            decimal minRequired = currentHighest + auction.IncrementoMinimo;
            if (amount < minRequired)
                return $"Tu puja debe ser al menos {minRequired:N2} Oz (incremento mínimo: {auction.IncrementoMinimo:N2} Oz).";

            // ─── REGISTRAR LA PUJA ──────────────────────────────────────────

            try
            {
                await _repository.MarkAllBidsAsNotLastAsync(auctionId);
                var bid = new AuctionBidHistory
                {
                    AuctionId = auctionId,
                    UserId = userId,
                    Amount = amount,
                    BidDate = DateTime.UtcNow,
                    IsLastBid = true
                };
                await _repository.AddBidAsync(bid);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                // Esto te muestra el error REAL de la base de datos
                var inner = ex.InnerException?.Message ?? ex.Message;
                return $"DB ERROR: {inner}";
            }

     
            return null; 
        }
    }
}