using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCore.Data;
using BookCore.Entities;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblOrdersController : ControllerBase
    {
        private readonly BookDbContext _context;

        public TblOrdersController(BookDbContext context)
        {
            _context = context;
        }

        // GET: api/TblOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblOrder>>> GetTblOrders()
        {
          if (_context.TblOrders == null)
          {
              return NotFound();
          }
            return await _context.TblOrders.ToListAsync();
        }

        // GET: api/TblOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblOrder>> GetTblOrder(string id)
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

            return tblOrder;
        }

        // PUT: api/TblOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblOrder(string id, TblOrder tblOrder)
        {
            if (id != tblOrder.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(tblOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblOrderExists(id))
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

        // POST: api/TblOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblOrder>> PostTblOrder(TblOrder tblOrder)
        {
          if (_context.TblOrders == null)
          {
              return Problem("Entity set 'BookDbContext.TblOrders'  is null.");
          }
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

            return CreatedAtAction("GetTblOrder", new { id = tblOrder.OrderId }, tblOrder);
        }

        // DELETE: api/TblOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblOrder(string id)
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

            _context.TblOrders.Remove(tblOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblOrderExists(string id)
        {
            return (_context.TblOrders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
