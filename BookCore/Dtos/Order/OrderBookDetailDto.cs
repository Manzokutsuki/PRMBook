using BookCore.Dtos.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Order
{
    public class OrderBookDetailDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public PublisherDto publisher { get; set; }
    }
}
