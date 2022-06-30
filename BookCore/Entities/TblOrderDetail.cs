using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblOrderDetail
    {
        public int OrderDetailId { get; set; }
        public string? OrderId { get; set; }
        public string? PublisherName { get; set; }
        public string? PublisherPhone { get; set; }
        public string? BookId { get; set; }
        public string? Quantity { get; set; }
        public string? Price { get; set; }
        public string? StatusId { get; set; }

        public virtual TblBook? Book { get; set; }
        public virtual TblOrder? Order { get; set; }
    }
}
