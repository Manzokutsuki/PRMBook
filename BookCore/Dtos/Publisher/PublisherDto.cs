using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCore.Dtos.Publisher
{
    public class PublisherDto
    {
        public PublisherDto()
        {

        }
        public PublisherDto(string? name, string? phone)
        {
            this.name = name;
            this.phone = phone;
        }
        public string? name { get; set; }
        public string? phone { get; set; }

    }
}
