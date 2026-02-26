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
    public class ServiceRole : IServiceRole
    {
        private readonly IRepositoryRole _repository;
        private readonly IMapper _mapper;

        public ServiceRole(IRepositoryRole repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<RolesDTO?> GetRolById(int id)
        {
            var role = await _repository.GetRolById(id);
            return _mapper.Map<RolesDTO>(role);
        }

        public async Task<ICollection<RolesDTO>> ListAsync()
        {
            var roles = await _repository.ListAsync();
            return _mapper.Map<ICollection<RolesDTO>>(roles);
        }
    }
}
