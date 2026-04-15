using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Infraestructure.Data;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace D_A.Infraestructure.Repository.Implementation
{
    public class RepositoryPayment : IRepositoryPayment
    {
        private readonly DAContext _context;

        public RepositoryPayment(DAContext context)
        {
            _context = context;
        }

        public async Task GeneratePayment(int auctionId)
        {
            var winner = await _context.AuctionWinner
                .FirstOrDefaultAsync(w => w.Actionid == auctionId);

            if (winner == null) return;

            var alreadyExists = await _context.Payment.AnyAsync(p => p.AuctionId == auctionId);
            if (alreadyExists) return;

            var payment = new Payment
            {
                AuctionId = auctionId,
                WinnerUserId = winner.Idauctionwinner,     
                Amount = (long)winner.Finalprice,
                PaymentStatusId = 2,
                DateCreated = DateTime.Now
            };

            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment> ConfirmPaymentAsync(int paymentId)
        {
            var payment = await _context.Payment.FirstOrDefaultAsync(p => p.PaymentId == paymentId);

            if (payment == null) return null;

            payment.PaymentStatusId = 1; // Paid
            payment.Dateconfirmed = DateTime.Now;

            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> GetPaymentByAuctionAsync(int auctionId)
        {
            return await _context.Payment
                .FirstOrDefaultAsync(p => p.AuctionId == auctionId);
        }
    }
}
