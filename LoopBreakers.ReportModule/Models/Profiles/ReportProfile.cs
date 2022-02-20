using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.DAL.Entities;

namespace LoopBreakers.ReportModule.Models.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<TransferReport, TransferReportDTO>().ReverseMap();
            CreateMap<ActivityReport, ActivityReportDTO>().ReverseMap();
        }
    }
}
