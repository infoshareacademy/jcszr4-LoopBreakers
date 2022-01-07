using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;

namespace LoopBreakers.WebApp.Mappings
{
    public class RecipientProfile : Profile
    {
        public RecipientProfile()
        {
            CreateMap<RecipientDTO, Recipient>().ReverseMap();
        }
    }
}
