using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserDTO, ApplicationUser>().ReverseMap();
            CreateMap<UserProfileDTO, ApplicationUser>().ReverseMap();
        }
    }
}
