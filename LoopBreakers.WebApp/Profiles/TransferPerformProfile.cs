using LoopBreakers.DAL.Entities;
using AutoMapper;
using LoopBreakers.WebApp.DTOs;

namespace LoopBreakers.WebApp.Profiles
{
    public class TransferPerformProfile : Profile
    {
        public TransferPerformProfile()
        {
            CreateMap<TransferPerformDTO, Transfer>().ForMember(d=>d.Id, o=>o.Ignore());
            CreateMap<RecipientDTO, TransferPerformDTO>();
        }
    }
}
