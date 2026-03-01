using AutoMapper;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_A.Application.Services.Implementations
{
    internal class ServiceQuality : IServiceQuality
    {



        private readonly IServiceQuality _repository;
        private readonly IMapper _mapper;

        public ServiceQuality(IServiceQuality repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        public async Task<QualitiesDTO> GetqualityById(int id)
        {
            var qualities = await _repository.GetqualityById(id);
            return _mapper.Map<QualitiesDTO>(qualities);
        }

        public async Task<ICollection<QualitiesDTO>> ListAsync()
        {
            var qualities = await _repository.ListAsync();

            return _mapper.Map<ICollection<QualitiesDTO>>(qualities);
        }
    }
}
