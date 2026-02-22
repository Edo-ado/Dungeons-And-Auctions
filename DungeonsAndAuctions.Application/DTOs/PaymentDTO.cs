using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record PaymentDTO
    {
        [DisplayName("PaymentId")]
        public int PaymentId { get; set; }
        
        [DisplayName("AuctionId")]
        public int? AuctionId { get; set; }

        [DisplayName("WinnerUserId")]
        public int? WinnerUserId { get; set; }

        [DisplayName("Amount")]
        public decimal? Amount { get; set; }

        [DisplayName("PaymentStatusId")]
        public int? PaymentStatusId { get; set; }

        [DisplayName("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [DisplayName("Dateconfirmed")]
        public DateTime? Dateconfirmed { get; set; }


    }
}
