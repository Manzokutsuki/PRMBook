using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.User
{
    public class BasicUserDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Uid { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? CreateDate { get; set; }
        public string? StatusId { get; set; }
        public string? Role { get; set; }
    }
}
