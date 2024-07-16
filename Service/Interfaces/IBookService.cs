using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBookService
    {
        IEnumerable<BookDto> GetAllBooks();
        void AddBook(BookDto book);
        void UpdateBook(BookDto bookDto);
        void DeleteBook(int id);
    }
}
