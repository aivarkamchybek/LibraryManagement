using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
        public AuthorViewModel Author { get; set; }
    }
}