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
        private readonly BookDbContext _context;
        private readonly IMapper _mapper;

        public TblBooksController(BookDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/TblBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblBook>>> GetTblBooks()
        {
          if (_context.TblBooks == null)
          {
              return NotFound();
          }
            return await _context.TblBooks.ToListAsync();
        }

        // GET: api/TblBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblBook>> GetTblBook(string id)
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

            return tblBook;
        }

        // PUT: api/TblBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblBook(string id, TblBook tblBook)
        {
            if (id != tblBook.Id)
            {
                return BadRequest();
            }

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
                return Problem("Entity set 'BookDbContext.TblBooks'  is null.");
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
    }
}
