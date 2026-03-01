using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record CategoriesDTO
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Name")]
        public string? Name { get; set; } = string.Empty;

        [DisplayName("Description")] 
        public string? Description { get; set; } = string.Empty;
    }
}
