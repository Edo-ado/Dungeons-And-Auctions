using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Repository.Interfaces;

namespace D_A.Application.Services.Implementations
{
    public class ServiceCountry : IServiceCountry
    {

        private readonly IServiceCountry _repository;
        private readonly IMapper _mapper;

        public ServiceCountry(IServiceCountry repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CountriesDTO> GetCountryById(int id)
        {
           var country = await _repository.GetCountryById(id);
            return _mapper.Map<CountriesDTO>(country);
        }

        public async Task<ICollection<CountriesDTO>> ListAsync()
        {
            var country = await _repository.ListAsync();
            return _mapper.Map<ICollection<CountriesDTO>>(country);
        }
    }
}
