using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblCarts = new HashSet<TblCart>();
            TblOrders = new HashSet<TblOrder>();
            TblReceivers = new HashSet<TblReceiver>();
        }

        public string UserId { get; set; } = null!;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Uid { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? CreateDate { get; set; }
        public string? StatusId { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<TblCart> TblCarts { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
        public virtual ICollection<TblReceiver> TblReceivers { get; set; }
    }
}
