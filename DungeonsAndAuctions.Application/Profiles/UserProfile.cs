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
        CreateMap<Users, UsersDTO>();
        CreateMap<UsersDTO, Users>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}
