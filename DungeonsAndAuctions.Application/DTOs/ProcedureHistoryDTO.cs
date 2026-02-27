using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record ProcedureHistoryDTO
    {

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("User Id")]
        public int UserId { get; set; }

        [DisplayName("Procedure Type")]
        public int ProcedureType { get; set; }

        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [DisplayName("Completion Date")]
        public DateOnly CompletionDate { get; set; } = new DateOnly(2000, 1, 1);

    }
}
