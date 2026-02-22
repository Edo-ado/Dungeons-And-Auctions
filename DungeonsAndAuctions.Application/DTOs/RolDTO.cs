using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record RoleDTO
    {
        [DisplayName("ID")]
        public int Id { get; init; }

        [DisplayName("Rol")]
        public string Name { get; init; } = string.Empty;

        [DisplayName("Descripción")]
        public string? Description { get; init; }  
    }
}
