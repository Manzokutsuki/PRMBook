using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCore.Data;
using BookCore.Entities;
using BookCore.Dtos.Cart;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblCartsController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public TblCartsController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TblCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetTblCarts()
        {
          if (_context.TblCarts == null)
          {
              return NotFound();
          }

          var tblCarts = await _context.TblCarts.ToListAsync();
            if (tblCarts == null)
            {
                return new List<CartDto>();
            }

            var CartDtos = new List<CartDto>();
            foreach (var cart in tblCarts)
            {
                var tempCartDto = _mapper.Map<CartDto>(cart);
                var tempCartItems = GetCartItems(cart.Id);
                tempCartDto.cartItems = tempCartItems;
                CartDtos.Add(tempCartDto);
            }

            return CartDtos;
        }

        // GET: api/TblCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCart>> GetTblCart(int id)
        {
          if (_context.TblCarts == null)
          {
              return NotFound();
          }
            var tblCart = await _context.TblCarts.FindAsync(id);

            if (tblCart == null)
            {
                return NotFound();
            }

            return tblCart;
        }

        // PUT: api/TblCarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCart(int id, TblCart tblCart)
        {
            if (id != tblCart.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblCart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCartExists(id))
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

        // POST: api/TblCarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblCart>> PostTblCart(TblCart tblCart)
        {
          if (_context.TblCarts == null)
          {
              return Problem("Entity set 'BookContext.TblCarts'  is null.");
          }
            _context.TblCarts.Add(tblCart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblCart", new { id = tblCart.Id }, tblCart);
        }

        // DELETE: api/TblCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCart(int id)
        {
            if (_context.TblCarts == null)
            {
                return NotFound();
            }
            var tblCart = await _context.TblCarts.FindAsync(id);
            if (tblCart == null)
            {
                return NotFound();
            }

            _context.TblCarts.Remove(tblCart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblCartExists(int id)
        {
            return (_context.TblCarts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private List<CartItemDto>? GetCartItems(int Id)
        {
            var cartItems = new List<CartItemDto>();
            var list = _context.TblCartItems.Where(item =>
            item.CartId == Id).ToList();
            if (list.Any() && list != null)
            {
                foreach (var item in list)
                {
                    var temp = _mapper.Map<CartItemDto>(item);
                    cartItems.Add(temp);
                }
            }
            return cartItems;
        }
    }
}
