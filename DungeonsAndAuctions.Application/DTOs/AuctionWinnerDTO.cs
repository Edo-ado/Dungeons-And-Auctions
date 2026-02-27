using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record AuctionWinnerDTO
    {

        [DisplayName("AuctionId")]
        public int Actionid { get; set; }

        [DisplayName("WinnerId")]
        public int Winnerid { get; set; }

        [DisplayName("Final Price")]
        public decimal Finalprice { get; set; }

        [DisplayName("Closed Date")]
        public DateTime Closeddate { get; set; } 

        [DisplayName("Bid Winning Id")]
        public string Bidwinningid { get; set; } = string.Empty;

        [DisplayName("Id")]
        public int Idauctionwinner { get; set; }
    }
}
