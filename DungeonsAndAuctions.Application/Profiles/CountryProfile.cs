using AutoMapper;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;
using System.Diagnostics.Metrics;



namespace D_A.Application.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            // Mapeo sencillo entre entidad y DTO (y viceversa si se necesita)
            CreateMap<Countries, CountriesDTO>();

            CreateMap<Countries, CountriesDTO>().ReverseMap();
        }
    }
}