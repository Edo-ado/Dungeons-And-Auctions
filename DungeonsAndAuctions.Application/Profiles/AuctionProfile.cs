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
           .ForMember(
               dest => dest.TotalBids,
               opt => opt.MapFrom(src => src.AuctionBidHistory != null
                   ? src.AuctionBidHistory.Count
                   : 0)
           );

            CreateMap<AuctionsDTO, Auctions>();
        }
    }
}