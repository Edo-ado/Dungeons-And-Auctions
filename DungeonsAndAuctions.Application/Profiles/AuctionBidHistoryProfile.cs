using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;
public class AuctionBidHistoryProfile : Profile
{
    public AuctionBidHistoryProfile()
    {
        CreateMap<AuctionBidHistory, AuctionBidHistoryDTO>();
        CreateMap<AuctionBidHistoryDTO, AuctionBidHistory>()
            .ForMember(dest => dest.AuctionWinner, opt => opt.Ignore());
    }
}