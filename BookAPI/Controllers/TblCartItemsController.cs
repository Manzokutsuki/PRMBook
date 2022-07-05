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
using BookCore.Dtos.Cart;
using BookInfrasture.Utils;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblCartItemsController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public TblCartItemsController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TblCartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetTblCartItems()
        {
          if (_context.TblCartItems == null)
          {
              return NotFound();
          }
          var cartItems = await _context.TblCartItems.ToListAsync();
            if (cartItems == null)
            {
                return new List<CartItemDto>();
            }
            var cartItemDtos = new List<CartItemDto>();
            foreach(var cartItem in cartItems)
            {
                var temp = _mapper.Map<CartItemDto>(cartItem);
                cartItemDtos.Add(temp);
            }

            return cartItemDtos;
        }

        // PUT: api/TblCartItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCartItem(int id, NewCartItemDto newCartItem)
        {
            if (id != newCartItem.CartId)
            {
                return BadRequest();
            }

            var tblCart = await _context.TblCarts.FindAsync(newCartItem.CartId);
            if (tblCart == null)
            {
                return Problem("Cart does not exist");
            }

            var tblCartItem = _mapper.Map<TblCartItem>(newCartItem);

            _context.Entry(tblCartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCartItemExists(id))
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

        // POST: api/TblCartItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewCartItemDto>> PostTblCartItem(NewCartItemDto newCartItem)
        {
          if (_context.TblCartItems == null)
          {
              return Problem("Entity set 'BookContext.TblCartItems'  is null.");
          }
            var tblCart = await _context.TblCarts.FindAsync(newCartItem.CartId);
            if (tblCart == null)
            {
                return Problem("Cart does not exist");
            }
            var tblCartItem = _mapper.Map<TblCartItem>(newCartItem);
            _context.TblCartItems.Add(tblCartItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblCartItemExists(tblCartItem.CartId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TblCartItems/5
        [HttpDelete()]
        public async Task<IActionResult> DeleteTblCartItem(NewCartItemDto newCartItem)
        {
            if (_context.TblCartItems == null)
            {
                return NotFound();
            }

            var tblCart = await _context.TblCarts.FindAsync(newCartItem.CartId);
            if (tblCart == null)
            {
                return Problem("Cart does not exist");
            }
            newCartItem.BookId = CommonUtils.FormatStringInput(newCartItem.BookId);
            var tblCartItem = await _context.TblCartItems.SingleOrDefaultAsync(cart =>
                cart.CartId.Equals(newCartItem.CartId)
                && cart.BookId.Equals(newCartItem.BookId));
            if (tblCartItem == null)
            {
                return NotFound();
            }

            var tblBook = IncreaseBookQuantity(newCartItem.BookId, Int32.Parse(newCartItem.Quantity));
            if (tblBook == null)
            {
                return Problem("Cannot increase book's quantity from removed cart");
            }
            _context.TblBooks.Update(tblBook);
            _context.TblCartItems.Remove(tblCartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblCartItemExists(int id)
        {
            return (_context.TblCartItems?.Any(e => e.CartId == id)).GetValueOrDefault();
        }

        private TblBook IncreaseBookQuantity(string BookID, int Quantity)
        {
            var tblBook = new TblBook();
            BookID = CommonUtils.FormatStringInput(BookID);
            tblBook = _context.TblBooks.SingleOrDefault(book => book.Id.ToLower().Equals(BookID));
            if (tblBook != null)
            {
                tblBook.Quantity += Quantity;
            }
            return tblBook;
        }
    }
}
