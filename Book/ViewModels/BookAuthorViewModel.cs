using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorAge { get; set; }
        public string AuthorCountry { get; set; }
    }
}