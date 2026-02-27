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
        //es el traductpr q le dice a automapper como convertir users a userDTO y así al reves
        //esto es necesario 
        CreateMap<Users, UsersDTO>()//users a DTO es cuando lees de una BN
               .ForMember(d => d.RoleName, opt => opt.MapFrom(src => src.Role.Name))
          .ForMember(d => d.GenderName,opt => opt.MapFrom(src => src.Gender.Name))
            .ForMember(d => d.CountryName, opt => opt.MapFrom(src => src.Country.Name)
 );  
      

        CreateMap<UsersDTO, Users>()//DTO a Users, es cuando guardas desde fomulario
            .ForMember(d => d.PasswordHash, opt => opt.Ignore());
    }
}
