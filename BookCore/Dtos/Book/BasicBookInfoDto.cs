using BookCore.Dtos.Category;
using BookCore.Dtos.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Book
{
    public class BasicBookInfoDto
    {
        public CategoryDto categoryDto { get; set; }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Supplier { get; set; }
        public PublisherDto publisher { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Language { get; set; }
        public string? Size { get; set; }
        public int? Page { get; set; }
        public int? ReleaseYear { get; set; }
        public DateTime? CreateDate { get; set; }
        public String? Status { get; set; }
    }
}
