using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs.Api
{
    public record UserApiDto
    {

        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; } = new DateOnly(2000, 1, 1);

        public int CountryId { get; set; }

        public int GenderId { get; set; }

        public string AboutMe { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public bool IsBlocked { get; set; }

        public DateOnly RegisterDate { get; set; } = new DateOnly(2000, 1, 1);

        public DateOnly LastLogin { get; set; } = new DateOnly(2000, 1, 1);

        public bool Active { get; set; }

        public int NumberCreatedAuctions { get; set; } //si es vendedor (cantidad de subastas creadas)

        public int NumberBidMade { get; set; }//si es comprador (cantidad de pujas realizadas)

        public string RoleName { get; set; } = string.Empty;

        public string CountryName { get; set; } = string.Empty;

        public string GenderName { get; set; } = string.Empty;

    }
}
