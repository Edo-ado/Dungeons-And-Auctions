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
                .ForMember(dest => dest.Auctions, opt => opt.MapFrom(src => src.Auctions))
                .ForMember(dest => dest.UserNameOwner, opt => opt.MapFrom(src => src. User.UserName))
                .ForMember(dest => dest.Imagenes, opt => opt.MapFrom(src => src.IdImageNavigation.Select(i => i.ImageData).ToList()))
                .ReverseMap();



            //mapeo para poder crear pues
            CreateMap<ObjectsDTO, Objects>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Auctions, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.IdImageNavigation, opt => opt.Ignore());


            CreateMap<Categories, CategoriesDTO>();
            CreateMap<CategoriesDTO, Categories>();

            CreateMap<Auctions, AuctionsDTO>();
      



        }
    }
}

