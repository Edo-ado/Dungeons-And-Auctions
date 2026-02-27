using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;

namespace D_A.Application.Services.Implementations
{
    public class ServiceGender : IServiceGender
    {

        private readonly IServiceGender _repository;
        private readonly IMapper _mapper;

        public ServiceGender(IServiceGender repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GendersDTO> GetGenderById(int id)
        {
            var gender = await _repository.GetGenderById(id);
            return _mapper.Map<GendersDTO>(gender);
        }

        public async Task<ICollection<GendersDTO>> ListAsync()
        {
            var gender = await _repository.ListAsync();
            return _mapper.Map<ICollection<GendersDTO>>(gender);
        }
    }
}
