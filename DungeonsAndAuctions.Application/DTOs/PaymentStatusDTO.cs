using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record PaymentStatusDTO
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; } = string.Empty;

    }
}
