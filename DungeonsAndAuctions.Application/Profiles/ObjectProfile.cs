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
    public class ObjectProfile : Profile
    {
        public ObjectProfile()
        {

            CreateMap<Objects, ObjectsDTO>();//users a DTO es cuando lees de una BN


        }
    }
}

