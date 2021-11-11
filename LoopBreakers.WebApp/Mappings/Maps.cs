using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.WebApp.Data;
using LoopBreakers.WebApp.DTOs;

namespace LoopBreakers.WebApp.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Transfer, TransferDTO>().ReverseMap();

        }
    }
}
