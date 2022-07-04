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
using BookCore.Dtos.Order;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblOrdersController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public TblOrdersController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TblOrders
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetTblOrders(string? userId)
        {
          if (_context.TblOrders == null)
          {
              return NotFound();
          }
            return GetTblOrdersByRequirements(userId).ToList();
        }

        // GET: api/TblOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetTblOrder(string id)
        {
          if (_context.TblOrders == null)
          {
              return NotFound();
          }
            var tblOrder = await _context.TblOrders.FindAsync(id);

            if (tblOrder == null)
            {
                return NotFound();
            }
            var OrderDetails = _context.TblOrderDetails.Where(orderDetail =>
            orderDetail.OrderId.Equals(id)).ToList();
            tblOrder.TblOrderDetails = OrderDetails;
            var orderDto = _mapper.Map<OrderDto>(tblOrder);

            return orderDto;
        }
        // POST: api/TblOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostTblOrder(OrderDto orderDto)
        {
          if (_context.TblOrders == null)
          {
              return Problem("Entity set 'BookContext.TblOrders'  is null.");
          }

            var tblOrder = _mapper.Map<TblOrder>(orderDto);
            _context.TblOrders.Add(tblOrder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblOrderExists(tblOrder.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblOrder", new { id = orderDto.OrderId }, orderDto);
        }

        private bool TblOrderExists(string id)
        {
            return (_context.TblOrders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }

        private IEnumerable<OrderDto> GetTblOrdersByRequirements(string userId)
        {
            var tempOrderList = new List<OrderDto>();
            if (userId != null && userId.Any())
            {
                userId = userId.ToLower().Trim();

                var Tblorders = _context.TblOrders
                    .Where(order => order.UserId.ToLower().Equals(userId))
                    .ToList();
                if (Tblorders == null)
                {
                    return new List<OrderDto>();
                }
                var TblorderDetailList = _context.TblOrderDetails.ToList();
                foreach (var order in Tblorders)
                {
                    var orderDetail = TblorderDetailList
                        .Where(detail => detail.OrderId.Equals(order.OrderId)).ToList();
                    var tempOrder = new TblOrder()
                    {
                        OrderId = order.OrderId,
                        UserId = order.UserId,
                        TotalMoney = order.TotalMoney,
                        Quantity = order.Quantity,
                        OrderDate = order.OrderDate,
                        Name = order.Name,
                        Phone = order.Phone,
                        Address = order.Address,
                        Email = order.Email,
                        TblOrderDetails = orderDetail
                    };
                    var tempOrderDto = _mapper.Map<OrderDto>(tempOrder);
                    tempOrderList.Add(tempOrderDto);
                }
                return tempOrderList;
            }
            else
            {
                var orders = _context.TblOrders.ToList();
                if (orders == null)
                {
                    return new List<OrderDto>();
                }
                var orderDetailList = _context.TblOrderDetails.ToList();
                foreach (var order in orders)
                {
                    var orderDetail = orderDetailList
                        .Where(detail => detail.OrderId.Equals(order.OrderId)).ToList();
                    var tempOrder = new TblOrder()
                    {
                        OrderId = order.OrderId,
                        UserId = order.UserId,
                        TotalMoney = order.TotalMoney,
                        Quantity = order.Quantity,
                        OrderDate = order.OrderDate,
                        Name = order.Name,
                        Phone = order.Phone,
                        Address = order.Address,
                        Email = order.Email,
                        TblOrderDetails = orderDetail
                    };
                    var tempOrderDto = _mapper.Map<OrderDto>(tempOrder);
                    tempOrderList.Add(tempOrderDto);
                }
            }
            return tempOrderList;
        }
    }
}
