using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblBooks = new HashSet<TblBook>();
        }

        public string CategoryId { get; set; } = null!;
        public string? CategoryName { get; set; }

        public virtual ICollection<TblBook> TblBooks { get; set; }
    }
}
