using AutoMapper;
using BookCore.Dtos.Receiver;
using BookCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInfrasture.Mappings
{
    public class AutomapperReceiver : Profile
    {
        public AutomapperReceiver()
        {
            CreateMap<TblReceiver, ReceiverDto>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
                .ReverseMap();
        }
    }
}
