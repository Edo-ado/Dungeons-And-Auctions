using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record AuctionStateDTO
    {
        [DisplayName("Id")]
        public int Idstate { get; set; }


        [DisplayName("Name")]
        public string? Name { get; set; } = string.Empty;
    }
}
