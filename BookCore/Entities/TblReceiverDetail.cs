using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblReceiverDetail
    {
        public TblReceiverDetail()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        public int Id { get; set; }
        public int? ReceiverId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        public virtual TblReceiver? Receiver { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
