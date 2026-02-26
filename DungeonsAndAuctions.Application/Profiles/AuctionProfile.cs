using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;
public class AuctionProfile : Profile
{
    public AuctionProfile()
    {
        CreateMap<Auctions, AuctionsDTO>();
        CreateMap<AuctionsDTO, Auctions>();
           
    }
}