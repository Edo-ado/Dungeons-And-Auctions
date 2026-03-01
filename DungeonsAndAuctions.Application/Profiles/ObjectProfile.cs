using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;

namespace D_A.Application.Profiles
{
    public class ObjectProfile : Profile
    {
        public ObjectProfile()
        {
            CreateMap<Objects, ObjectsDTO>().ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Auctions, opt => opt.MapFrom(src => src.Auctions));

            CreateMap<Categories, CategoriesDTO>();
            CreateMap<Auctions, AuctionsDTO>();
        }
    }
}

