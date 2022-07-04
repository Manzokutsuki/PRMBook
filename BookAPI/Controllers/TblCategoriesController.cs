using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCore.Data;
using BookCore.Dtos.Category;
using AutoMapper;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblCategoriesController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public TblCategoriesController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TblCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetTblCategories()
        {
          if (_context.TblCategories == null)
          {
              return NotFound();
          }

          var tblCategories = await _context.TblCategories.ToListAsync();
            List<CategoryDto> categories = new List<CategoryDto>();
            foreach (var category in tblCategories)
            {
                var temp = _mapper.Map<CategoryDto>(category);
                categories.Add(temp);
            }

            return categories;
        }

        // GET: api/TblCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetTblCategory(string id)
        {
          if (_context.TblCategories == null)
          {
              return NotFound();
          }
            var tblCategory = await _context.TblCategories.FindAsync(id);

            if (tblCategory == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(tblCategory);

            return categoryDto;
        }
    }
}
