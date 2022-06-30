using AutoMapper;
using BookCore.Dtos.User;
using BookCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInfrasture.Mappings
{
    public class AutoMapperUser:Profile
    {
        public AutoMapperUser()
        {
            CreateMap<TblUser, BasicUserDto>().ReverseMap();
        }
    }
}
