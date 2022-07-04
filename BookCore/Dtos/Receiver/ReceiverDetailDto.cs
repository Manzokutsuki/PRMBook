using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Receiver
{
    public class ReceiverDetailDto
    {
        public int? Id { get; set; }
        public int? ReceiverId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }

        public ReceiverDetailDto()
        {}

        public ReceiverDetailDto(int? id, int? receiverId, string? name, string? phone, string? address, string? email)
        {
            Id = id;
            ReceiverId = receiverId;
            Name = name;
            Phone = phone;
            Address = address;
            Email = email;
        }

        public ReceiverDetailDto(int? id, string? name, string? phone, string? address, string? email)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Address = address;
            Email = email;
        }
    }
}
