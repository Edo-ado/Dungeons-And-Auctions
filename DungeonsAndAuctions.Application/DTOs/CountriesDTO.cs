using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.DTOs
{
    public record CountriesDTO
    {
        [DisplayName("ID")]
        public int Id { get; init; }

        [DisplayName("País")]
        public string Name { get; init; } = string.Empty;

        [DisplayName("Código")]
        public int CountryCode { get; init; }
    }
}
