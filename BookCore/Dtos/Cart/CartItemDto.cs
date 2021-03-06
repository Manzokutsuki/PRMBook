using BookCore.Dtos.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Cart
{
    public class CartItemDto
    {
        public int? CartId { get; set; }
        public string? BookId { get; set; }
        public string? BookName { get; set; }
        public string? Image { get; set; }
        public string? Quantity { get; set; }
        public string? Price { get; set; }
        public string? StatusId { get; set; }
        public string? BookTotalQuantity { get; set; }
        public PublisherDto publisher { get; set; }
        public CartItemDto()
        {
        }

        public CartItemDto(int? cartId, string? bookId, string? bookName, string? image, string? quantity, string? price, string? statusId, string? bookTotalQuantity, PublisherDto publisher)
        {
            CartId = cartId;
            BookId = bookId;
            BookName = bookName;
            Image = image;
            Quantity = quantity;
            Price = price;
            StatusId = statusId;
            BookTotalQuantity = bookTotalQuantity;
            this.publisher = publisher;
        }
    }
}
