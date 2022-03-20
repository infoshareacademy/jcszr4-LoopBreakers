using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.DTOs;

namespace LoopBreakers.WebApp.Profiles
{
    public class TransferReportProfile : Profile
    {
        public TransferReportProfile()
        {
            CreateMap<TransferPerformDTO, TransferReportDTO>().ReverseMap();
        }
    }
}
