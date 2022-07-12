using AutoMapper;
using BookCore.Entities;
using BookCore.Dtos.Cart;
using BookCore.Data;
using BookCore.Dtos.Publisher;

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
                    dest.Image = GetBookImage(src.BookId);
                    var publisherName = GetPublisherName(src.BookId);
                    var publisherPhone = GetPublisherPhone(src.BookId);
                    PublisherDto publisher = new PublisherDto()
                    {
                        name = publisherName,
                        phone = publisherPhone
                    };
                    dest.publisher = publisher;
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
                .ForMember(dest => dest.StatusId, act => act.MapFrom(src => src.StatusId))
                .ReverseMap();
            CreateMap<NewCartItemDto, CartItemDto>()
                .ForMember(dest => dest.CartId, act => act.MapFrom(src => src.CartId))
                .ForMember(dest => dest.BookId, act => act.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Image, act => act.MapFrom(src => src.Image))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                .ForMember(dest => dest.StatusId, act => act.MapFrom(src => src.StatusId))
                .AfterMap((src, dest) =>
                {
                    dest.BookName = GetBookName(src.BookId);
                    var publisherName = GetPublisherName(src.BookId);
                    var publisherPhone = GetPublisherPhone(src.BookId);
                    PublisherDto publisher = new PublisherDto()
                    {
                        name = publisherName,
                        phone = publisherPhone
                    };
                    dest.publisher = publisher;
                });
        }

        public String? GetBookName(String Id)
        {
            using (var context = new BookContext())
            {
                var BookName = context.TblBooks.Find(Id).Name;
                return BookName != null ? BookName : null;
            }
        }

        public String? GetBookImage(string? BookID)
        {
            using (var context = new BookContext())
            {
                var image = context.TblBooks.Find(BookID).Image;
                return image == null ? null : image;
            };
        }

        public String? GetPublisherName(string? BookID)
        {
            using (var context = new BookContext())
            {
                var name = context.TblBooks.Find(BookID).PublisherName;
                return name == null ? null : name;
            };
        }
        public String? GetPublisherPhone(string? BookID)
        {
            using (var context = new BookContext())
            {
                var phone = context.TblBooks.Find(BookID).PublisherPhone;
                return phone == null ? null : phone;
            };
        }


    }
}
