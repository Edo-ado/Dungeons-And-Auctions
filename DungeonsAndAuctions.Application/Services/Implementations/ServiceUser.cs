using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Infraestructure.Repository.Interfaces;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;

namespace D_A.Application.Services.Implementations
{
    public class ServiceUser : IServiceUser
    {
        private readonly IRepositoryUser _repository;
        private readonly IMapper _mapper;

        public ServiceUser(IRepositoryUser repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UsersDTO>> ListAsync()
        {
            var users = await _repository.ListAsync();
            return _mapper.Map<List<UsersDTO>>(users);
        }


        public async Task<UsersDTO> FindByIdAsync(int id)  
        {
            var User = await _repository.FindByIdAsync(id);
            return _mapper.Map<UsersDTO>(User);
        }

    }
}
