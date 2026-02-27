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
        //es el traductpr q le dice a automapper como convertir users a userDTO y así al reves
        //esto es necesario 
        CreateMap<AuctionBidHistory, AuctionBidHistoryDTO>();//users a DTO es cuando lees de una BN
 


        CreateMap<AuctionBidHistoryDTO, AuctionBidHistory>();
         
    }
}
