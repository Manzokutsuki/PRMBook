using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Order
{
    public class OrderDto
    {
        public string? OrderId { get; set; }
        public string? UserId { get; set; }
        public string? TotalMoney { get; set; }
        public string? Quantity { get; set; }

        public OrderDetailDto OrderDetail { get; set; }

        public OrderDto()
        {}

        public OrderDto(string? orderId, string? userId, 
            string? totalMoney, string? quantity,
            OrderDetailDto orderDetail)
        {
            OrderId = orderId;
            UserId = userId;
            TotalMoney = totalMoney;
            Quantity = quantity;
            OrderDetail = orderDetail;
        }
    }
}
