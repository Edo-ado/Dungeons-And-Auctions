using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;

namespace DNDA.Web.Models.Api
{
    public class ObjectApiDto
    {

       
        public int Id { get; set; }

        public int UserId { get; set; }


      
        public string Name { get; set; } = string.Empty;


   
        public int? Year { get; set; }

        public string? Description { get; set; } = string.Empty;

        public string? UserNameOwner { get; set; } = string.Empty;


        public DateOnly RegistrationDate { get; set; }

        public decimal MarketPrice { get; set; }

        public bool IsActive { get; set; }

        public List<CategoriesDTO> Categories { get; set; } = new();

        public int? IdState { get; set; }

        public int? Idimage { get; set; }

        public Qualities? IdQualityNavigation { get; set; }

        public List<byte[]> Imagenes { get; set; } = new List<byte[]>();

    
        public List<AuctionsDTO> Auctions { get; set; } = new();

    }
}
