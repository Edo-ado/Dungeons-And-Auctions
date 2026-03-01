using D_A.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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


        [DisplayName("UserNameOwner")]
        public string? UserNameOwner { get; set; } = string.Empty;


        [DisplayName("RegistrationDate")]
        public DateOnly RegistrationDate { get; set; }

        [DisplayName("MarketPrice")]
        public decimal MarketPrice { get; set; }

        [DisplayName("IsActive")]
        public bool IsActive { get; set; }

        [DisplayName("Categories")]
        public List<CategoriesDTO> Categories { get; set; } = new();

        [DisplayName("IdState")]
        public int? IdState { get; set; }

        [DisplayName("Idimage")]
        public int? Idimage { get; set; }

        public Qualities? IdQualityNavigation { get; set; }

        [Display(Name = "Imagen Libro")]
        public byte[] Imagen { get; set; } = Array.Empty<byte>();

        [DisplayName("AuctionsObjects")]
        public List<AuctionsDTO> Auctions { get; set; } = new();


    }
}
