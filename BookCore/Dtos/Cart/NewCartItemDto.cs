using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Cart
{
    public class NewCartItemDto
    {
        public int? CartId { get; set; }
        public string? BookId { get; set; }
        public string? Image { get; set; }
        public string? Quantity { get; set; }
        public string? Price { get; set; }
        public string? StatusId { get; set; }

        public NewCartItemDto()
        {}

        public NewCartItemDto(int? cartId, string? bookId, string? image, string? quantity, string? price, string? statusId)
        {
            CartId = cartId;
            BookId = bookId;
            Image = image;
            Quantity = quantity;
            Price = price;
            StatusId = statusId;
        }
    }
}
