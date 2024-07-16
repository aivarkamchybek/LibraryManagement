using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
