using AutoMapper;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;

namespace D_A.Application.Profiles
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<Auctions, AuctionsDTO>()

                .ForMember(d => d.ObjectName, opt => opt.MapFrom(src => src.IdobjectNavigation.Name))
                .ForMember(d => d.StateName, opt => opt.MapFrom(src => src.IdstateNavigation.Name))
                .ForMember(d => d.UserCreatorName, opt => opt.MapFrom(src => src.IdusercreatorNavigation.UserName));
                
               
            





            CreateMap<AuctionsDTO, Auctions>();
        }
    }
}