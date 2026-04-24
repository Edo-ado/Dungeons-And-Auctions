// Path: DungeonsAndAuctions.Infraestructure/Models/RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace DNDA.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres.")]
        [Display(Name = "Nombre de Pila")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(60, ErrorMessage = "Máximo 60 caracteres.")]
        [Display(Name = "Apellido del Linaje")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El alias es obligatorio.")]
        [StringLength(40, ErrorMessage = "Máximo 40 caracteres.")]
        [Display(Name = "Alias en el Reino")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [Display(Name = "Correo Arcano")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debes confirmar tu contraseña.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateOnly BirthDate { get; set; } = new DateOnly(2000, 1, 1);

        [Phone(ErrorMessage = "Número de teléfono inválido.")]
        [Display(Name = "Número de Mensajería")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "País de Origen")]
        public int CountryId { get; set; }

        [Display(Name = "Género")]
        public int GenderId { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        [Display(Name = "Sobre ti")]
        public string? AboutMe { get; set; }

        // RoleId: 1 = Comprador, 2 = Vendedor
        [Required(ErrorMessage = "Debes elegir un rol.")]
        [Display(Name = "Rol en el Reino")]
        public int RoleId { get; set; } = 1;
    }
}