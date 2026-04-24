

namespace D_A.Application.DTOs
{
    public record RegisterUserDTO
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty; // contraseña en texto plano
        public DateOnly BirthDate { get; init; }
        public int CountryId { get; init; }
        public int GenderId { get; init; }
        public int RoleId { get; init; } = 1;
        public string? PhoneNumber { get; init; }
        public string? AboutMe { get; init; }
    }
}