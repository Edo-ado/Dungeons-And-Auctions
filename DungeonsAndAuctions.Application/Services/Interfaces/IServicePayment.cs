using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;

namespace D_A.Application.Services.Interfaces
{
    public interface IServicePayment
    {

        Task GeneratePayment(int auctionId);
        Task<PaymentDTO> ConfirmPaymentAsync(int paymentId);

        Task<PaymentDTO> GetPaymentByAuctionAsync(int auctionId);




    }
}
