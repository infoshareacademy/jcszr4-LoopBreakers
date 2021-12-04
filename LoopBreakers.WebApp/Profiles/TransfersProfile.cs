using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.WebApp.Data;
using LoopBreakers.WebApp.DTOs;

namespace LoopBreakers.WebApp.Mappings
{
    public class TransfersProfile : Profile
    {
        public TransfersProfile()
        {
            CreateMap<Transfer, TransferDTO>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ReverseMap();
        }
    }
}
