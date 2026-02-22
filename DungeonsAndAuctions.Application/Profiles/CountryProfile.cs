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
    public class CountryProfile:Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryDTO, Countries>().ReverseMap();
        }
    }
}
