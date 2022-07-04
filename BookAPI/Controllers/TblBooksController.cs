using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCore.Data;
using BookCore.Entities;
using AutoMapper;
using BookCore.Dtos.Book;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblBooksController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public TblBooksController(BookContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/TblBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasicBookInfoDto>>> GetBooks(string? Name, String? CategoryId)
        {
          if (_context.TblBooks == null)
          {
              return NotFound();
          }

            List<BasicBookInfoDto> BasicBooks = new List<BasicBookInfoDto>();
            var Books = await _context.TblBooks.OrderBy(book => book.Status).ToListAsync();
            Books = GetListBySearch(Name, CategoryId, Books);
            if (Books.Count() > 0)
            {
                foreach (var book in Books)
                {
                    var Temp = _mapper.Map<BasicBookInfoDto>(book);
                    BasicBooks.Add(Temp);
                }
            }
            return BasicBooks;
        }

        // GET: api/TblBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BasicBookInfoDto>> GetTblBook(string id)
        {
          if (_context.TblBooks == null)
          {
              return NotFound();
          }
            var temp = await _context.TblBooks.FindAsync(id);

            if (temp == null)
            {
                return NotFound();
            }

            var tblBook = _mapper.Map<BasicBookInfoDto>(temp);
            return tblBook;
        }

        // PUT: api/TblBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblBook(string id, BasicBookInfoDto basicBook)
        {
            if (id != basicBook.Id)
            {
                return BadRequest();
            }

            var tblBook = _mapper.Map<TblBook>(basicBook);

            _context.Entry(tblBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TblBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblBook>> PostTblBook(BasicBookInfoDto basicBookInfoDto)
        {
            if (_context.TblBooks == null)
            {
                return Problem("Entity set 'BookContext.TblBooks'  is null.");
            }

            var book = _mapper.Map<TblBook>(basicBookInfoDto);
            book.Id = System.Guid.NewGuid().ToString();

            _context.TblBooks.Add(book);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblBookExists(book.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblBook", new { id = book.Id }, book);
        }

        // DELETE: api/TblBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblBook(string id)
        {
            if (_context.TblBooks == null)
            {
                return NotFound();
            }
            var tblBook = await _context.TblBooks.FindAsync(id);
            if (tblBook == null)
            {
                return NotFound();
            }

            _context.TblBooks.Remove(tblBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblBookExists(string id)
        {
            return (_context.TblBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private List<TblBook> GetListBySearch(string? Name, String? CategoryId, List<TblBook>Books)
        {
            if (Name != null && CategoryId != null)
            {
                Name = Name.Trim().ToLower();
                CategoryId = CategoryId.Trim().ToLower();
                var ListTemp = new List<TblBook>();
                foreach (var book in Books)
                {
                    var Temp = book.CategoryId;
                    if (Temp != null)
                    {
                        if (Temp.Equals(CategoryId) &&
                            book.Name.ToLower().Contains(Name))
                        {
                            ListTemp.Add(book);
                        }
                    }
                }
            }
            else if (Name != null)
            {
                Name = Name.Trim().ToLower();
                Books = Books.Where(book => book.Name.ToLower().Contains(Name)).ToList();
            }
            else if (CategoryId != null)
            {
                CategoryId = CategoryId.Trim().ToLower();
                var ListTemp = new List<TblBook>();
                foreach (var book in Books)
                {
                    var Temp = book.CategoryId;
                    if (Temp != null)
                    {
                        if (Temp.Equals(CategoryId))
                        {
                            ListTemp.Add(book);
                        }
                    }
                }
                /*Books = ListTemp;*/
            }
            return Books;
        }
    }
}
