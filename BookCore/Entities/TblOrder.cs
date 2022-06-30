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
        public int? ReceiverDetailId { get; set; }
        public string? TotalMoney { get; set; }
        public string? Quantity { get; set; }
        public string? OrderDate { get; set; }
        public string? StatusId { get; set; }

        public virtual TblReceiverDetail? ReceiverDetail { get; set; }
        public virtual TblUser? User { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
