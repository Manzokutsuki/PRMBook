using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCore.Data;
using BookCore.Dtos.Receiver;
using AutoMapper;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblReceiversController : ControllerBase
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public TblReceiversController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/TblReceivers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceiverDto>>> GetTblReceivers()
        {
          if (_context.TblReceivers == null)
          {
              return NotFound();
          }

          var receiverList = await _context.TblReceivers.ToListAsync();
          var receiverDtoList = new List<ReceiverDto>();
            if (receiverList.Count > 0)
            {
                foreach (var receiver in receiverList)
                {
                    var tempReceiverDto = _mapper.Map<ReceiverDto>(receiver);
                    var tempDetailList = GetDetailList(receiver.Id);
                    if (tempDetailList != null)
                    {
                        tempReceiverDto.Detail = tempDetailList;
                    }
                    receiverDtoList.Add(tempReceiverDto);
                }
            }

            return receiverDtoList;
        }

        // GET: api/TblReceivers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceiverDto>> GetTblReceiver(int id)
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
            var receiverDto = _mapper.Map<ReceiverDto>(tblReceiver);
            var tempDetailList = GetDetailList(id);
            if (tempDetailList != null)
            {
                receiverDto.Detail = tempDetailList;
            }

            return receiverDto;
        }

        private List<ReceiverDetailDto>? GetDetailList(int? ReceiverId)
        {
            var list = _context.TblReceiverDetails.Where(detail =>
            detail.ReceiverId.Equals(ReceiverId)).ToList();
            if (list.Count > 0)
            {
                var detailDto = new List<ReceiverDetailDto>();
                foreach(var item in list)
                {
                    var temp = _mapper.Map<ReceiverDetailDto>(item);
                    detailDto.Add(temp);
                }
                return detailDto;
            }
            return null;
        }
    }
}
