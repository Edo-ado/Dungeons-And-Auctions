using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record UsersDTO
    {
        // el DTO es el modelo q llega a la vista (no es el modelo DB)
        //ES NADA MÁS LO Q SALE A LA VISTA, aqui podemos añadir atributos q necesitemos
        //

        [DisplayName("ID User")]
        public int Id { get; set; }

        [DisplayName("UserName")]
        public string UserName { get; set; } = string.Empty;

        [DisplayName("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [DisplayName("LastName")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("PasswordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [DisplayName("BirthDate")]
        public DateOnly BirthDate { get; set; } = new DateOnly(2000, 1, 1);

        [DisplayName("CountryID")]
        public int CountryId { get; set; }

        [DisplayName("GenderID")]
        public int GenderId { get; set; }

        [DisplayName("AboutMe")]
        public string AboutMe { get; set; } = string.Empty;

        [DisplayName("Email")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("PhoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        [DisplayName("RoleID")]
        public int RoleId { get; set; }



        [DisplayName("IsBlocked")]
        public bool IsBlocked { get; set; }

        [DisplayName("RegisterDate")]
        public DateOnly RegisterDate { get; set; } = new DateOnly(2000, 1, 1);

        [DisplayName("LastLogin")]
        public DateOnly LastLogin { get; set; } = new DateOnly(2000, 1, 1);

        [DisplayName("Active")]
        public bool Active { get; set; }

        [DisplayName("NumberCreatedAuctions")]
        public int NumberCreatedAuctions { get; set; } //si es vendedor (cantidad de subastas creadas)

        [DisplayName("NumberBidMade")]
        public int NumberBidMade{ get; set; }//si es comprador (cantidad de pujas realizadas)




        [DisplayName("RoleName")]
        public string RoleName { get; set; } = string.Empty;

        [DisplayName("CountryName")]
        public string CountryName { get; set; } = string.Empty;

        [DisplayName("GenderName")]
        public string GenderName { get; set; } = string.Empty;


    }
}
