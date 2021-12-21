using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;

namespace LoopBreakers.WebApp.Mappings
{
    public class TransfersProfile : Profile
    {
        public TransfersProfile()
        {
            CreateMap<TransferDTO, Transfer>().ReverseMap();
        }
    }
}
