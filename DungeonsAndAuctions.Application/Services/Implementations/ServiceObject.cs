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

        public async Task<int> AddAsync(ObjectsDTO entity, List<int> selectedCategorias, List<byte[]> imagenes)
        {
            var entityy = _mapper.Map<Objects>(entity);
            return await _repository.AddAsync(entityy, selectedCategorias, imagenes);
        }

     

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<Objects?> FindByIdAsync(int id)
        {
            var ojb = await _repository.FindByIdAsync(id);
            return _mapper.Map<Objects>(ojb);
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

        public async Task<bool> HasActiveAuctionAsync(int objectId)
        {
            return await _repository.HasActiveAuctionAsync(objectId);
        }

        public async Task<bool> HasBeenAuctionedAsync(int objectId)
        {
            return await _repository.HasBeenAuctionedAsync(objectId);
        }

        public async Task<ICollection<ObjectsDTO>> ListActiveAsync()
        {
            var objects = await _repository.ListActiveAsync();
            return _mapper.Map<List<ObjectsDTO>>(objects);
        }

        public async Task<ICollection<ObjectsDTO>> ListAsync()
        {
            var objects = await _repository.ListAsync();
            return _mapper.Map<List<ObjectsDTO>>(objects);
        }

        public async Task ToggleActiveAsync(int id)
        {
            await _repository.ToggleActiveAsync(id);
        }

        public async Task UpdateAsync(int id, ObjectsDTO dto, List<int> selectedCategorias, List<byte[]> imagenes)
        {
            var entity = await _repository.FindByIdAsync(id);
            if (entity is null) throw new Exception("Objeto no encontrado");
            _mapper.Map(dto, entity);
            await _repository.UpdateAsync(entity, selectedCategorias, imagenes);
        }

     
    }
}
