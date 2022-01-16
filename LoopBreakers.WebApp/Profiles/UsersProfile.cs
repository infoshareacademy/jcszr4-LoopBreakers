using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Migrations;
using LoopBreakers.WebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Mappings
{
    public class UsersProfile : Profile
    {

       public UsersProfile()
       {
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();
       }

    }
}
