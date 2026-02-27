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
        public int idstate { get; set; }

        [DisplayName("IdUserCreator")]
        public int idusercreator { get; set; }

        [DisplayName("IdObject")]
        public int idobject { get; set; }



        [DisplayName("Object Name")]
        public string ObjectName { get; set; } = string.Empty;

        [DisplayName("State Name")]
        public string StateName { get; set; } = string.Empty;

        [DisplayName("User Creator Name")]
        public string UserCreatorName { get; set; } = string.Empty;

        

        public List<AuctionBidHistoryDTO> BidHistory { get; set; } = new List<AuctionBidHistoryDTO>();


        [DisplayName("Categories")]
        public List<CategoriesDTO> Categories { get; set; } = new List<CategoriesDTO>();



    }
}
