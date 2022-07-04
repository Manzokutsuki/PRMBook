using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblCart
    {
        public TblCart()
        {
            TblCartItems = new HashSet<TblCartItem>();
        }

        public int Id { get; set; }
        public string? UserId { get; set; }

        public virtual TblUser? User { get; set; }
        public virtual ICollection<TblCartItem> TblCartItems { get; set; }
    }
}
