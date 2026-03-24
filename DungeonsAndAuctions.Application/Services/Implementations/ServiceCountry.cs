using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace D_A.Application.Services.Implementations
{
    public class ServiceCountry : IServiceCountry
    {
        private readonly DAContext _context;
        private readonly IMapper _mapper;

        public ServiceCountry(DAContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountriesDTO> GetCountryById(int id)
        {
            var country = await _context.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<CountriesDTO>(country);
        }

        public async Task<ICollection<CountriesDTO>> ListAsync()
        {
            var countries = await _context.Countries
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<ICollection<CountriesDTO>>(countries);
        }
    }
}