using AutoMapper;
using BookCore.Entities;
using BookCore.Dtos.Category;

namespace BookInfrasture.Mappings
{
    public class AutoMapperCategory : Profile
    {
        public AutoMapperCategory()
        {
            CreateMap<TblCategory, CategoryDto>()
                .ReverseMap();
        }
    }
}
