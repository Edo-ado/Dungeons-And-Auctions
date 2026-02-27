using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record ObjectsDTO
    {

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("UserId")]
        public int UserId { get; set; }


        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Year")]
        public int? Year { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; } = string.Empty;

        [DisplayName("RegistrationDate")]
        public DateOnly RegistrationDate { get; set; }

        [DisplayName("MarketPrice")]
        public decimal MarketPrice { get; set; }

        [DisplayName("IsActive")]
        public bool IsActive { get; set; }


        [DisplayName("IdState")]
        public int? IdState { get; set; }

        [DisplayName("Idimage")]
        public int? Idimage { get; set; }

        public List<CategoriesDTO> Categories { get; set; } = new List<CategoriesDTO>();


    }
}
