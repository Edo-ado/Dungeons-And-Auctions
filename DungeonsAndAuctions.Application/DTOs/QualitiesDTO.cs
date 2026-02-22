using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    internal class QualitiesDTO
    {

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Quality")]
        public string? Quality { get; set; } = string.Empty;
    }
}
