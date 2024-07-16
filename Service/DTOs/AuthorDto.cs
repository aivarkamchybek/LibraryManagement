using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.DTOs;

namespace Service
{
    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public ICollection<BookDto> Books { get; set; }
    }
}
