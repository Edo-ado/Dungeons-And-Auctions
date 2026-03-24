using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Users, UsersDTO>()
               .ForMember(d => d.RoleName, opt => opt.MapFrom(src => src.Role.Name))
          .ForMember(d => d.GenderName,opt => opt.MapFrom(src => src.Gender.Name))
          .ForMember(d => d.NumberCreatedAuctions, opt => opt.MapFrom(src => src.Auctions != null ? src.Auctions.Count : 0))
            .ForMember(d => d.NumberBidMade, opt => opt.MapFrom(src => src.AuctionBidHistory != null ? src.AuctionBidHistory.Count : 0))
            .ForMember(d => d.CountryName, opt => opt.MapFrom(src => src.Country.Name)
            
 );



        CreateMap<UsersDTO, Users>()
            .ForMember(d => d.PasswordHash, opt => opt.Ignore())
            .ForMember(d => d.Role, opt => opt.Ignore())
            .ForMember(d => d.Country, opt => opt.Ignore())
            .ForMember(d => d.Gender, opt => opt.Ignore())
            .ForMember(d => d.AuctionBidHistory, opt => opt.Ignore())
            .ForMember(d => d.Auctions, opt => opt.Ignore())
            .ForMember(d => d.Objects, opt => opt.Ignore())
            .ForMember(d => d.ProceduresHistory, opt => opt.Ignore());
    }
}
