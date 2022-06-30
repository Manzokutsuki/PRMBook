using AutoMapper;
using BookCore.Dtos.Book;
using BookCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInfrasture.Mappings
{
    public class AutoMapperBook :Profile
    {
        public AutoMapperBook()
        {
            CreateMap<TblBook, BasicBookInfoDto>().ReverseMap();
        }
    }
}
