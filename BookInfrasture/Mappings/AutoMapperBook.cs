using AutoMapper;
using BookCore.Dtos.Book;
using BookCore.Entities;
using BookCore.Data;
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
            CreateMap<TblBook, BasicBookInfoDto>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Supplier, act => act.MapFrom(src => src.Supplier))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Image, act => act.MapFrom(src => src.Image))
                .ForMember(dest => dest.Language, act => act.MapFrom(src => src.Language))
                .ForMember(dest => dest.Size, act => act.MapFrom(src => src.Size))
                .ForMember(dest => dest.Page, act => act.MapFrom(src => src.Page))
                .ForMember(dest => dest.ReleaseYear, act => act.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.CreateDate, act => act.MapFrom(src => src.CreateDate))
                .AfterMap((src, dest) =>
                {
                    dest.Status = GetStatus(src.Status);
                    var name = GetCategoryName(src.CategoryId);
                    dest.categoryDto = new BookCore.Dtos.Category.CategoryDto(src.CategoryId, name);
                    dest.publisher = new BookCore.Dtos.Publisher.PublisherDto(src.AuthorName, src.PublisherPhone);
                })
                ;

            CreateMap<BasicBookInfoDto, TblBook>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.Supplier, act => act.MapFrom(src => src.Supplier))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                .ForMember(dest => dest.Image, act => act.MapFrom(src => src.Image))
                .ForMember(dest => dest.Language, act => act.MapFrom(src => src.Language))
                .ForMember(dest => dest.Size, act => act.MapFrom(src => src.Size))
                .ForMember(dest => dest.Page, act => act.MapFrom(src => src.Page))
                .ForMember(dest => dest.ReleaseYear, act => act.MapFrom(src => src.ReleaseYear))
                .ForMember(dest => dest.CreateDate, act => act.MapFrom(src => src.CreateDate))
                .ForMember(dest => dest.Status, act => act.MapFrom(src => GetByte(src.Status)))
                .ForMember(dest => dest.CategoryId, act => act.MapFrom(src => src.categoryDto.CategoryId))
                .ForMember(dest => dest.PublisherName, act => act.MapFrom(src => src.publisher.name))
                .ForMember(dest => dest.PublisherPhone, act => act.MapFrom(src => src.publisher.phone))
                .AfterMap((src, dest) =>
                {
                    dest.Status = GetByte(src.Status);
                    dest.CategoryId = src.categoryDto.CategoryId;
                    dest.PublisherName = src.publisher.name;
                    dest.PublisherPhone = src.publisher.phone;
                });
        }

        private String? GetStatus(byte? Status)
        {
            String result = Status == 0 ? 
                BookStatus.InStock.ToEnumMemberAttrValue().ToString() : 
                BookStatus.OutOfStock.ToEnumMemberAttrValue().ToString();
            return result;
        }

        private Byte GetByte(string? Status)
        {
            return Status.Equals(BookStatus.InStock.ToEnumMemberAttrValue()) ? 
                (byte)BookStatus.InStock 
                : (byte)BookStatus.OutOfStock;
        }

        private String? GetCategoryName(String? id)
        {
            using (BookContext context = new BookContext())
            {
                var category = context.TblCategories.Find(id);
                if (category != null)
                {
                    return category.CategoryName;
                }
            }
            return null;
        }
    }
}
