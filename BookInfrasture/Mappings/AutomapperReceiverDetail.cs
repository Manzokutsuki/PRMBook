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
    public class AutomapperReceiverDetail : Profile
    {
        public AutomapperReceiverDetail()
        {
            CreateMap<TblReceiverDetail, ReceiverDetailDto>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReceiverId, act => act.MapFrom(src => src.ReceiverId))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, act => act.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, act => act.MapFrom(src => src.Address))
                .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                .ReverseMap();
        }
    }
}
