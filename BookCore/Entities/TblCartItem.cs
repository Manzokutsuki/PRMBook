using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblCartItem
    {
        public int CartId { get; set; }
        public string BookId { get; set; } = null!;
        public string? Quantity { get; set; }
        public string? Price { get; set; }
        public string? StatusId { get; set; }

        public virtual TblBook Book { get; set; } = null!;
        public virtual TblCart Cart { get; set; } = null!;
    }
}
