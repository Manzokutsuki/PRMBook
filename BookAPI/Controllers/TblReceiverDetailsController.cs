using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCore.Data;
using BookCore.Entities;
using BookCore.Dtos.Receiver;
using AutoMapper;
using BookInfrasture.Utils;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblReceiverDetailsController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public TblReceiverDetailsController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TblReceiverDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiverDetailDto>>> GetTblReceiverDetails()
        {
          if (_context.TblReceiverDetails == null)
          {
              return NotFound();
          }
            var receiverDetails = await _context.TblReceiverDetails.ToListAsync();
            var receiverDetailsDto = new List<ReceiverDetailDto>();
            foreach (var receiver in receiverDetails)
            {
                var temp = _mapper.Map<ReceiverDetailDto>(receiver);
                receiverDetailsDto.Add(temp);
            } 
            return receiverDetailsDto;
        }

        // GET: api/TblReceiverDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiverDetailDto>> GetTblReceiverDetail(int id)
        {
          if (_context.TblReceiverDetails == null)
          {
              return NotFound();
          }
            var tblReceiverDetail = await _context.TblReceiverDetails.FindAsync(id);

            if (tblReceiverDetail == null)
            {
                return NotFound();
            }

            var receiverDetailDto = _mapper.Map<ReceiverDetailDto>(tblReceiverDetail);

            return receiverDetailDto;
        }

        // PUT: api/TblReceiverDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblReceiverDetail(int id, ReceiverDetailDto tblReceiverDetailDto)
        {
            if (id != tblReceiverDetailDto.Id)
            {
                return BadRequest();
            }

            tblReceiverDetailDto.Name = CommonUtils.FormatStringInput(tblReceiverDetailDto.Name);
            tblReceiverDetailDto.Email = CommonUtils.FormatStringInput(tblReceiverDetailDto.Email);
            tblReceiverDetailDto.Phone = CommonUtils.FormatStringInput(tblReceiverDetailDto.Phone);
            tblReceiverDetailDto.Address = CommonUtils.FormatStringInput(tblReceiverDetailDto.Address);


            var checkExisted = _context.TblReceiverDetails.Any(receiver =>
            receiver.Id != id &&
            receiver.ReceiverId == tblReceiverDetailDto.ReceiverId &&
            receiver.Name.ToLower().Equals(tblReceiverDetailDto.Name) &&
            receiver.Email.ToLower().Equals(tblReceiverDetailDto.Email) &&
            receiver.Phone.ToLower().Equals(tblReceiverDetailDto.Phone) &&
            receiver.Address.ToLower().Equals(tblReceiverDetailDto.Address)
            );

            if (checkExisted)
            {
                return Conflict("Duplicated other receiver's information");
            }

            var tblReceiverDetail = _mapper.Map<TblReceiverDetail>(tblReceiverDetailDto);
            _context.Entry(tblReceiverDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblReceiverDetailExists(id))
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

        // POST: api/TblReceiverDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReceiverDetailDto>> PostTblReceiverDetail(ReceiverDetailDto dto)
        {
          if (_context.TblReceiverDetails == null)
          {
              return Problem("Entity set 'BookContext.TblReceiverDetails'  is null.");
          }
            var tblReceiverDetail = _mapper.Map<TblReceiverDetail>(dto);

            tblReceiverDetail.Name = CommonUtils.FormatStringInput(tblReceiverDetail.Name);
            tblReceiverDetail.Email = CommonUtils.FormatStringInput(tblReceiverDetail.Email);
            tblReceiverDetail.Phone = CommonUtils.FormatStringInput(tblReceiverDetail.Phone);
            tblReceiverDetail.Address = CommonUtils.FormatStringInput(tblReceiverDetail.Address);

            var checkExisted = _context.TblReceiverDetails.Any(receiver =>
            receiver.ReceiverId == tblReceiverDetail.ReceiverId &&
            receiver.Name.ToLower().Equals(tblReceiverDetail.Name) &&
            receiver.Email.ToLower().Equals(tblReceiverDetail.Email) &&
            receiver.Phone.ToLower().Equals(tblReceiverDetail.Phone) &&
            receiver.Address.ToLower().Equals(tblReceiverDetail.Address)
            );

            if (checkExisted)
            {
                return Conflict("Duplicated receiver information");
            }

            _context.TblReceiverDetails.Add(tblReceiverDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblReceiverDetail", new { id = tblReceiverDetail.Id }, tblReceiverDetail);
        }

        // DELETE: api/TblReceiverDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblReceiverDetail(int id)
        {
            if (_context.TblReceiverDetails == null)
            {
                return NotFound();
            }
            var tblReceiverDetail = await _context.TblReceiverDetails.FindAsync(id);
            if (tblReceiverDetail == null)
            {
                return NotFound();
            }

            _context.TblReceiverDetails.Remove(tblReceiverDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblReceiverDetailExists(int id)
        {
            return (_context.TblReceiverDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
