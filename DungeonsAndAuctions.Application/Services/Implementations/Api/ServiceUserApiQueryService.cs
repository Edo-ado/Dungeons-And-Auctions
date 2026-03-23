using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs.Api;
using D_A.Application.Services.Interfaces.Api;
using D_A.Infraestructure.Repository.Interfaces;

namespace D_A.Application.Services.Implementations.Api
{
    public class ServiceUserApiQueryService : IUserApiQueryService
    {


        private readonly IRepositoryUser _repository;
        private readonly IMapper _mapper;

        public ServiceUserApiQueryService(IRepositoryUser repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<UserApiDto?> FindByIdAsync(int id)
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity is null) return null;

            return _mapper.Map<UserApiDto>(entity);
        }

        public async Task<ICollection<UserApiDto>> ListAsync()
        {
            {
                var list = await _repository.ListAsync();
                return _mapper.Map<ICollection<UserApiDto>>(list);
            }
        }
    }

}
