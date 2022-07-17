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
        [HttpGet("{userId}")]
        public async Task<ActionResult<CartDto>> GetTblCart(string userId, string? bookId)
        {
            userId = userId.ToLower().Trim();
          if (_context.TblCarts == null)
          {
              return NotFound();
          }
            var tblCart = await _context.TblCarts.SingleOrDefaultAsync(cart =>
            cart.UserId.ToLower().Equals(userId));

            if (tblCart == null)
            {
                return NotFound();
            }

            return GetCartDto(tblCart, bookId);
        }

        private CartDto GetCartDto(TblCart tblCart, string? bookId)
        {
            var cartDto = _mapper.Map<CartDto>(tblCart);
            if (bookId != null)
            {
                var tempCartItemWithBooks = GetCartItems(tblCart.Id, bookId);
                cartDto.cartItems = tempCartItemWithBooks;
                return cartDto;
            }
            var tempCartItems = GetCartItems(tblCart.Id);
            cartDto.cartItems = tempCartItems;
            return cartDto;
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

        private List<CartItemDto>? GetCartItems(int Id, string bookId)
        {
            var cartItems = new List<CartItemDto>();
            var list = _context.TblCartItems.Where(item =>
            item.CartId == Id && item.BookId.Equals(bookId)).ToList();
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
