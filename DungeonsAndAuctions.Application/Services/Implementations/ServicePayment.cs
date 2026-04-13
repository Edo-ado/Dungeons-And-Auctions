using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;

namespace D_A.Application.Services.Implementations
{
    public class ServicePayment : IServicePayment
    {

        private readonly IRepositoryPayment _repository;
        private readonly IMapper _mapper;

        public ServicePayment(IRepositoryPayment repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaymentDTO> ConfirmPaymentAsync(int paymentId)
        {
            var payment = await _repository.ConfirmPaymentAsync(paymentId);
            return _mapper.Map<PaymentDTO>(payment);
        }

        public async Task GeneratePayment(int auctionId)
        {
            await _repository.GeneratePayment(auctionId);
        }

        public async Task<PaymentDTO> GetPaymentByAuctionAsync(int auctionId)
        {
            var payment = await _repository.GetPaymentByAuctionAsync(auctionId);
            return _mapper.Map<PaymentDTO>(payment);
        }
    }
}
