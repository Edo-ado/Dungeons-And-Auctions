using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using D_A.Application.DTOs;
using D_A.Infraestructure.Models;

namespace D_A.Application.Profiles
{
    public class GenderProfile:Profile
    {
        public GenderProfile()
        {
            CreateMap<GenderDTO, Genders>().ReverseMap();
        }
    }
}
