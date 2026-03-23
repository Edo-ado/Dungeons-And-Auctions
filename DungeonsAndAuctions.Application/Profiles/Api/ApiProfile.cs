using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs.Api;

using D_A.Infraestructure.Models;

namespace D_A.Application.Profiles.Api
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<Categories, CategorieApiDto>();

            CreateMap<Users, UserApiDto>()
    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
    .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
    .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name))
    .ForMember(dest => dest.NumberCreatedAuctions, opt => opt.MapFrom(src => src.Auctions.Count))
    .ForMember(dest => dest.NumberBidMade, opt => opt.MapFrom(src => src.AuctionBidHistory.Count));

        }
    }
}
