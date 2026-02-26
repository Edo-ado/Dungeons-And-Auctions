using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record AuctionsDTO
    {


        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Start Date")]
        public DateOnly StartDate { get; set; } = new DateOnly();
        [DisplayName("End Date")]
        public DateOnly EndDate { get; set; } = new DateOnly();
        [DisplayName("Active?")]
        public bool IsActive { get; set; }

        [DisplayName("Base price")]
        public decimal BasePrice { get; set; } = 0;

        [DisplayName("Minimum bid")]
        public decimal IncrementoMinimo { get; set; } = 0;

        [DisplayName("IdState")]
        public int Idstate { get; set; }

        [DisplayName("IdUserCreator")]
        public int Idusercreator { get; set; }

        [DisplayName("IdObject")]
        public int Idobject { get; set; }


    }
}
