using AutoMapper;
using BookCore.Entities;
using BookCore.Dtos.Cart;
using BookCore.Data;

namespace BookInfrasture.Mappings
{
    public class AutoMapperCart : Profile
    { 
        public AutoMapperCart()
        {
            CreateMap<TblCart, CartDto>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
                .ReverseMap();

            CreateMap<TblCartItem, CartItemDto>()
                .ForMember(dest => dest.CartId, act => act.MapFrom(src => src.CartId))
                .ForMember(dest => dest.BookId, act => act.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                .ForMember(dest => dest.StatusId, act => act.MapFrom(src => src.StatusId))
                .AfterMap((src, dest) =>
                {
                    dest.BookName = GetBookName(src.BookId);
                });

            CreateMap<CartItemDto, TblCartItem>()
                .ForMember(dest => dest.CartId, act => act.MapFrom(src => src.CartId))
                .ForMember(dest => dest.BookId, act => act.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                .ForMember(dest => dest.StatusId, act => act.MapFrom(src => src.StatusId));
            CreateMap<NewCartItemDto, TblCartItem>()
                .ForMember(dest => dest.CartId, act => act.MapFrom(src => src.CartId))
                .ForMember(dest => dest.BookId, act => act.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                .ForMember(dest => dest.StatusId, act => act.MapFrom(src => src.StatusId));
        }

        public String? GetBookName(String Id)
        {
            using (var context = new BookContext())
            {
                var BookName = context.TblBooks.Find(Id).Name;
                return BookName != null ? BookName : null;
            }
        }
    }
}
