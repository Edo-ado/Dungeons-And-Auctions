using D_A.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record AuctionBidHistoryDTO
    {
        [DisplayName("UserId")]
        public int UserId { get; set; }

        [DisplayName("AuctionId")]
        public int AuctionId { get; set; }
        [DisplayName("BidDate")]
        public DateTime BidDate { get; set; }
        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [DisplayName("IsLastBid")]
        public bool IsLastBid { get; set; }

        [DisplayName("Id")]
        public int Id { get; set; }

        public  Auctions Auction { get; set; } = null!;


        [DisplayName("User")]
        public  Users User { get; set; } = null!;
    }
}
