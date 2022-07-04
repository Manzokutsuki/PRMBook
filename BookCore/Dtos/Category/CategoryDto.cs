using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Category
{
    public class CategoryDto
    {
        public CategoryDto()
        {

        }

        public CategoryDto(string categoryId, string? categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }

        public string CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
