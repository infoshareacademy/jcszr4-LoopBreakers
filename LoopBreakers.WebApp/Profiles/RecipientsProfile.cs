using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Profiles
{
    public class RecipientsProfile : Profile
    {
        public RecipientsProfile()
        {
            CreateMap<RecipientDTO, Recipient>().ReverseMap();
        }
    }
}
