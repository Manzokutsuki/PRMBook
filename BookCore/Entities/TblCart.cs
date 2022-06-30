using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblCart
    {
        public int Id { get; set; }
        public string? UserId { get; set; }

        public virtual TblUser? User { get; set; }
    }
}
