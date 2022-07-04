using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblOrderDetails = new HashSet<TblOrderDetail>();
        }

        public string OrderId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? TotalMoney { get; set; }
        public string? Quantity { get; set; }
        public string? OrderDate { get; set; }
        public string? StatusId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        public virtual TblUser? User { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
