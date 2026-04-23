using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDA.Web.Models.Reports
{
    public class BuyerActivity
    {
        public string BuyerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int AuctionsParticipated { get; set; }
        public int TotalBids { get; set; }
    }

    public class SystemActivity
    {
        public int AuctionsCreated { get; set; }
        public int BidsPlaced { get; set; }
        public int AuctionsFinished { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class CombinedReportRequest
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
