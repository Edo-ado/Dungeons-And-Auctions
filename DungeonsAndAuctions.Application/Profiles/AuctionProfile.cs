using AutoMapper;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;

namespace D_A.Application.Profiles
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            CreateMap<Auctions, AuctionsDTO>();
         
            CreateMap<AuctionsDTO, Auctions>();
        }
    }
}