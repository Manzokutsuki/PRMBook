using System;
using System.Collections.Generic;

namespace BookCore.Entities
{
    public partial class TblBook
    {
        public TblBook()
        {
            TblCartItems = new HashSet<TblCartItem>();
            TblOrderDetails = new HashSet<TblOrderDetail>();
        }

        public string Id { get; set; } = null!;
        public string? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public string? PublisherName { get; set; }
        public string? PublisherPhone { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Language { get; set; }
        public string? Size { get; set; }
        public int? Page { get; set; }
        public int? ReleaseYear { get; set; }
        public DateTime? CreateDate { get; set; }
        public byte? Status { get; set; }
        public string? AuthorName { get; set; }

        public virtual TblCategory? Category { get; set; }
        public virtual ICollection<TblCartItem> TblCartItems { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
