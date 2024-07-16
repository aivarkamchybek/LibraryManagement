using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.ViewModels
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public List<BookViewModel> Books { get; set; }
    }
}