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
using BookCore.Dtos.User;
using BookInfrasture.Utils;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblUsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly BookContext _context;

        public TblUsersController(IMapper mapper, BookContext context)
        {
            this._mapper = mapper;
            this._context = context;
        }

        // GET: api/TblUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasicUserDto>>> GetTblUsers()
        {
          if (_context.TblUsers == null)
          {
              return NotFound();
          }
            var userList = await _context.TblUsers.ToListAsync();
            var userDtoList = new List<BasicUserDto>();
            foreach (var user in userList)
            {
                var temp = _mapper.Map<BasicUserDto>(user);
                userDtoList.Add(temp);
            }

            return userDtoList;
        }

        // GET: api/TblUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BasicUserDto>> GetTblUser(string id)
        {
          if (_context.TblUsers == null)
          {
              return NotFound();
          }
            var tblUser = await _context.TblUsers.FindAsync(id);

            if (tblUser == null)
            {
                return NotFound();
            }

            BasicUserDto basicUserDto = _mapper.Map<BasicUserDto>(tblUser);

            return basicUserDto;
        }

        // PUT: api/TblUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUser(string id, BasicUserDto basicUserDto)
        {
            if (id.Equals(basicUserDto.UserId) == false)
            {
                return BadRequest();
            }

            if (!TblUserExists(id))
            {
                return NotFound();
            }

            TblUser tblUser = _mapper.Map<TblUser>(basicUserDto);

            //_context.Entry(tblUser).State = EntityState.Modified;

            try
            {
                _context.TblUsers.Update(tblUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw (ex);
            }

            return NoContent();
        }

        // POST: api/TblUsers/signup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("/api/[controller]/signup")]
        [HttpPost]
        public async Task<ActionResult<BasicUserDto>> PostTblUser(BasicUserDto basicUserDto)
        {
          if (_context.TblUsers == null)
          {
              return Problem("Entity set 'BookContext.TblUsers'  is null.");
          }
          if (AccountExists(basicUserDto.Email, basicUserDto.Uid))
            {
                return Conflict("User exists");
            }
            var user = _mapper.Map<TblUser>(basicUserDto);
            user.UserId = System.Guid.NewGuid().ToString();
            try
            {
                var receiver = AddReceiver(user);
                var cart = AddCart(user);
                user.TblReceivers.Add(receiver);
                user.TblCarts.Add(cart);
                _context.TblUsers.Add(user);
                await _context.SaveChangesAsync();
                AddReceiverDetail(user);
                return CreatedAtAction("GetTblUser", new { id = user.UserId }, user);
            }
            catch (DbUpdateException)
            {
                if (TblUserExists(user.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
        }

        //POST: api/TblUsers/signin
        [Route("/api/[controller]/signin")]
        [HttpPost()]
        public async Task<ActionResult<BasicUserDto>> SignIn(LoginUserDto loginUser)
        {
            if (_context.TblUsers == null)
            {
                return NotFound();
            }
            loginUser.Email = CommonUtils.FormatStringInput(loginUser.Email);
            var tblUser = await _context.TblUsers.SingleAsync(temp => temp.Email.ToLower().Equals(loginUser.Email)
            || temp.Uid.Equals(loginUser.Uid.Trim()));
            if (tblUser == null)
            {
                return NotFound();
            }
            var basicUserDto = _mapper.Map<BasicUserDto>(tblUser);
            return basicUserDto;
        }

        // DELETE: api/TblUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUser(string id)
        {
            if (_context.TblUsers == null)
            {
                return NotFound();
            }
            var user = await _context.TblUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                var receiver = GetReceiver(user);
                var cart = GetCart(user);
                user.TblReceivers.Add(receiver);
                user.TblCarts.Add(cart);
                _context.TblUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            return NoContent();
        }

        private bool TblUserExists(string id)
        {
            return (_context.TblUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        private bool AccountExists(String? email, String? uid)
        {
            email = CommonUtils.FormatStringInput(email);
            return (_context.TblUsers?
                .Any(e => e.Email.ToLower().Equals(email) || e.Uid.Equals(uid)))
                .GetValueOrDefault();
        }

        private TblReceiver AddReceiver(TblUser user)
        {
            TblReceiver receiver = new TblReceiver()
            {
                UserId = user.UserId
            };
            return receiver;
        }

        private void AddReceiverDetail(TblUser user)
        {
            var index = _context.TblReceivers.Max(r => r.Id);
            TblReceiverDetail receiverDetail = new TblReceiverDetail()
            {
                Address = user.Address,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone,
                ReceiverId = index,
            };
            _context.TblReceiverDetails.Add(receiverDetail);
            _context.SaveChanges();
        }

        private TblCart AddCart(TblUser user)
        {
            TblCart cart = new TblCart()
            {
                UserId = user.UserId
            };
            return cart;
        }

        private TblReceiver GetReceiver(TblUser user)
        {
            TblReceiver receiver = _context.TblReceivers.FirstOrDefault(r => r.UserId == user.UserId);
            if (receiver != null)
            {
                IEnumerable<TblReceiverDetail> receiverDetail = _context.TblReceiverDetails.Where(detail => detail.ReceiverId == receiver.Id);
                if (receiverDetail.Any() && receiverDetail != null)
                {
                    receiver.TblReceiverDetails = receiverDetail.ToList();
                }
            }
            return receiver;
        }

        private TblCart GetCart(TblUser user)
        {
            TblCart cart = _context.TblCarts.SingleOrDefault(cart => cart.UserId == user.UserId);
            return cart;
        }
    }
}
