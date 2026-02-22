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
        public DateOnly BidDate { get; set; } = new DateOnly(2000, 1, 1);

        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [DisplayName("IsLastBid")]
        public bool IsLastBid { get; set; }

        [DisplayName("Id")]
        public string Id { get; set; } = string.Empty;

    }
}
