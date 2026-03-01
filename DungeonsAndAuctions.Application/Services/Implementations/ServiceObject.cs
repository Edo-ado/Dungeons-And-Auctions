using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs;
using D_A.Application.Services.Interfaces;
using D_A.Infraestructure.Models;
using D_A.Infraestructure.Repository.Interfaces;

namespace D_A.Application.Services.Implementations
{
    public class ServiceObject : IServiceObject
    {
        private readonly IRepositoryObject _repository;
        private readonly IMapper _mapper;

        public ServiceObject(IRepositoryObject repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Categories?> GetCategoriesByIdObject(int id)
        {
            var categories = await _repository.GetCategoriesByIdObject(id);
            return _mapper.Map<Categories>(categories);
        }

        public async Task<ObjectsDTO> GetObjectById(int id)
        {
            var objects = await _repository.FindByIdAsync(id);
            return _mapper.Map<ObjectsDTO>(objects);
        }

        public async Task<ICollection<ObjectsDTO>> ListAsync()
        {
            var objects = await _repository.ListAsync();
            return _mapper.Map<List<ObjectsDTO>>(objects);
        }
    }
}
