using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public class UserAuctionParticipationReportDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int AuctionsParticipated { get; set; }
        public int TotalValidBids { get; set; }
    }
}
