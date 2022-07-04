using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Receiver
{
    public class ReceiverDto
    {
        public int? Id { get; set; }
        public string? UserId { get; set; }

        public List<ReceiverDetailDto> Detail { get; set; }

        public ReceiverDto()
        {

        }

        public ReceiverDto(int? id, string? userId, List<ReceiverDetailDto> detail)
        {
            Id = id;
            UserId = userId;
            Detail = detail;
        }
    }
}
