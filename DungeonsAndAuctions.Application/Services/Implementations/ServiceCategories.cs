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
    public class ServiceCategories : IServiceCategories
    {


        private readonly IRepositoryCategory _repository;
        private readonly IMapper _mapper;

        public ServiceCategories(IRepositoryCategory repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoriesDTO> GetCategorieById(int id)
        {

            var categories = await _repository.GetCategoriesByID(id);

            return _mapper.Map<CategoriesDTO>(categories);
        }


        public async Task<ICollection<CategoriesDTO>> ListAsync()
        {
            var categories = await _repository.ListAsync();

            return _mapper.Map<ICollection<CategoriesDTO>>(categories);
        }



    }
}
