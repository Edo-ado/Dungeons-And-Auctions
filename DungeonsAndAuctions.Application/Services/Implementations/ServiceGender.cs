using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace D_A.Application.Services.Implementations
{
    public class ServiceGender : IServiceGender
    {
        private readonly DAContext _context;
        private readonly IMapper _mapper;

        public ServiceGender(DAContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GendersDTO> GetGenderById(int id)
        {
            var gender = await _context.Genders
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            return _mapper.Map<GendersDTO>(gender);
        }

        public async Task<ICollection<GendersDTO>> ListAsync()
        {
            var genders = await _context.Genders
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<ICollection<GendersDTO>>(genders);
        }
    }
}