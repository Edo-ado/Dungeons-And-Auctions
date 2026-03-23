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
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Name { get; set; } = string.Empty;


        [DisplayName("Year")]
        public int? Year { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "La descripción es requerida")]
        [MinLength(20, ErrorMessage = "La descripción debe tener mínimo 20 caracteres")]
        public string? Description { get; set; } = string.Empty;

        [DisplayName("UserNameOwner")]
        public string? UserNameOwner { get; set; } = string.Empty;


        [DisplayName("RegistrationDate")]
        public DateOnly RegistrationDate { get; set; }

        [DisplayName("MarketPrice")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal MarketPrice { get; set; }

        [DisplayName("IsActive")]
        public bool IsActive { get; set; }

        [DisplayName("Categories")]
        public List<CategoriesDTO> Categories { get; set; } = new();

        [DisplayName("IdState")]
        public int? IdState { get; set; }

        [DisplayName("StateName")]
        public string? StateName { get; set; }

        [DisplayName("Idimage")]
        public int? Idimage { get; set; }

        public Qualities? IdQualityNavigation { get; set; }

        [Display(Name = "Imagen Libro")]
        public List<byte[]> Imagenes { get; set; } = new List<byte[]>();

        [DisplayName("AuctionsObjects")]
        public List<AuctionsDTO> Auctions { get; set; } = new();


    }
}
