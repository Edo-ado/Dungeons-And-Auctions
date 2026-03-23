using AutoMapper;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;



namespace D_A.Application.Profiles
{
    public class GenderProfile : Profile
    {
        public GenderProfile()
        {
            // Mapeo sencillo entre entidad y DTO (y viceversa si se necesita)
            CreateMap<Genders, GendersDTO>();
            CreateMap<GendersDTO, Genders>();
        }
    }
}