using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }

        public List<CartItemDto>? cartItems { get; set; }

        public CartDto()
        {}

        public CartDto(int id, string? userId, List<CartItemDto>? cartItems)
        {
            Id = id;
            UserId = userId;
            this.cartItems = cartItems;
        }
    }
}
