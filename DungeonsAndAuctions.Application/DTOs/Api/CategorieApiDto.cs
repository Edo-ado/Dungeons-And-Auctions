using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs.Api
{
    public record CategorieApiDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

    }
}
