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
    public class TblReceiversController : ControllerBase
    {
        private readonly BookDbContext _context;

        public TblReceiversController(BookDbContext context)
        {
            _context = context;
        }

        // GET: api/TblReceivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblReceiver>>> GetTblReceivers()
        {
          if (_context.TblReceivers == null)
          {
              return NotFound();
          }
            return await _context.TblReceivers.ToListAsync();
        }

        // GET: api/TblReceivers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblReceiver>> GetTblReceiver(int id)
        {
          if (_context.TblReceivers == null)
          {
              return NotFound();
          }
            var tblReceiver = await _context.TblReceivers.FindAsync(id);

            if (tblReceiver == null)
            {
                return NotFound();
            }

            return tblReceiver;
        }

        // PUT: api/TblReceivers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblReceiver(int id, TblReceiver tblReceiver)
        {
            if (id != tblReceiver.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblReceiver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblReceiverExists(id))
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

        // POST: api/TblReceivers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblReceiver>> PostTblReceiver(TblReceiver tblReceiver)
        {
          if (_context.TblReceivers == null)
          {
              return Problem("Entity set 'BookDbContext.TblReceivers'  is null.");
          }
            _context.TblReceivers.Add(tblReceiver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblReceiver", new { id = tblReceiver.Id }, tblReceiver);
        }

        // DELETE: api/TblReceivers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblReceiver(int id)
        {
            if (_context.TblReceivers == null)
            {
                return NotFound();
            }
            var tblReceiver = await _context.TblReceivers.FindAsync(id);
            if (tblReceiver == null)
            {
                return NotFound();
            }

            _context.TblReceivers.Remove(tblReceiver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblReceiverExists(int id)
        {
            return (_context.TblReceivers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
