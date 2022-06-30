using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblReceiver
    {
        public TblReceiver()
        {
            TblReceiverDetails = new HashSet<TblReceiverDetail>();
        }

        public int Id { get; set; }
        public string? UserId { get; set; }

        public virtual TblUser? User { get; set; }
        public virtual ICollection<TblReceiverDetail> TblReceiverDetails { get; set; }
    }
}
