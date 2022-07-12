using AutoMapper;
using BookCore.Data;
using BookCore.Dtos.Order;
using BookCore.Dtos.Publisher;
using BookCore.Dtos.Receiver;
using BookCore.Entities;

namespace BookInfrasture.Mappings
{
    public class AutoMapperOrder : Profile
    {
        public AutoMapperOrder()
        {
            CreateMap<TblOrder, OrderDto>()
                .ForMember(dest => dest.OrderId, act => act.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
                .ForMember(dest => dest.TotalMoney, act => act.MapFrom(src => src.TotalMoney))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                .AfterMap((src, dest) =>
                {
                    var OrderId = src.OrderId;
                    var OrderDate = src.OrderDate;
                    var OrderDetailId = src.TblOrderDetails.First().OrderDetailId;
                    var ReceiverName = src.Name;
                    var ReceiverAddress = src.Address;
                    var ReceiverPhone = src.Phone;
                    var ReceiverEmail = src.Email;
                    var ReceiverDetail = new ReceiverDetailDto()
                    {
                        Name = ReceiverName,
                        Address = ReceiverAddress,
                        Email = ReceiverEmail,
                        Phone = ReceiverPhone,
                    };
                    var bookList = new List<OrderBookDetailDto>();
                    foreach (var orderDetail in src.TblOrderDetails)
                    {
                        var BookID = orderDetail.BookId;
                        var BookName = GetBookName(BookID);
                        var BookImage = GetBookImage(BookID);
                        var BookQuantity = orderDetail.Quantity;
                        var BookPrice = orderDetail.Price;
                        var publisherName = orderDetail.PublisherName;
                        var publisherPhone = orderDetail.PublisherPhone;
                        var orderBookDetailDto = new OrderBookDetailDto()
                        {
                            Id = BookID,
                            Name = BookName,
                            Image = BookImage,
                            Quantity = Int32.Parse(BookQuantity),
                            Price = Int32.Parse(BookPrice),
                            publisher = new PublisherDto()
                            {
                                name = publisherName,
                                phone = publisherPhone
                            }
                        };
                        bookList.Add(orderBookDetailDto);
                    }
                    dest.OrderDetail = new OrderDetailDto()
                    {
                        OrderId = OrderId,
                        OrderDate = OrderDate,
                        OrderDetailId = OrderDetailId,
                        receiverDetail = ReceiverDetail,
                        bookDetails = bookList
                    };
                });

            CreateMap<OrderDto, TblOrder>()
            .ForMember(dest => dest.OrderId, act => act.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.UserId, act => act.MapFrom(src => src.UserId))
            .ForMember(dest => dest.TotalMoney, act => act.MapFrom(src => src.TotalMoney))
            .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
            .AfterMap((src, dest) =>
            {
                var OrderDate = src.OrderDetail.OrderDate;
                var Name = src.OrderDetail.receiverDetail.Name;
                var Phone = src.OrderDetail.receiverDetail.Phone;
                var Address = src.OrderDetail.receiverDetail.Address;
                var Email = src.OrderDetail.receiverDetail.Email;
                dest.OrderDate = OrderDate;
                dest.Name = Name;
                dest.Phone = Phone;
                dest.Address = Address;
                dest.Email = Email;
            });

            CreateMap<OrderDetailDto, TblOrderDetail>()
                .ForMember(dest => dest.OrderDetailId, act => act.MapFrom(src => src.OrderDetailId))
                .ForMember(dest => dest.OrderId, act => act.MapFrom(src => src.OrderId))
                .AfterMap((src, dest) =>
                {
                    foreach(var book in src.bookDetails)
                    {
                        var name = book.publisher.name;
                        var phone = book.publisher.phone;
                        dest.PublisherName = name;
                        dest.PublisherPhone = phone;
                        dest.BookId = book.Id;
                        dest.Quantity = book.Quantity.ToString();
                        dest.Price = book.Price.ToString();
                    }
                });
        }

        public String? GetBookName(string? BookID)
        {
            using (var context = new BookContext())
            {
                var name = context.TblBooks.Find(BookID).Name;
                return name == null ? null : name;
            };
        }

        public String? GetBookImage(string? BookID)
        {
            using (var context = new BookContext())
            {
                var image = context.TblBooks.Find(BookID).Image;
                return image == null ? null : image;
            };
        }
    }
}
