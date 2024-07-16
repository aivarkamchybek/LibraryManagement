using Service.DTOs;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;

namespace Service
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<BookDto> GetAllBooks()
        {
            var books = _unitOfWork.Books.GetAll();
            return books.Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Genre = b.Genre,
                PublishDate = b.PublishDate,
                AuthorId = b.AuthorId,
                Author = b.Author != null ? new AuthorDto
                {
                    AuthorId = b.Author.AuthorId,
                    Name = b.Author.Name,
                    Age = b.Author.Age,
                    Country = b.Author.Country
                } : null
            });
        }


        public void AddBook(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Genre = bookDto.Genre,
                PublishDate = bookDto.PublishDate,
                AuthorId = bookDto.AuthorId
            };
            _unitOfWork.Books.Insert(book);
            _unitOfWork.Save();
        }
        public void UpdateBook(BookDto bookDto)
        {
            var existingBook = _unitOfWork.Books.GetAll().FirstOrDefault(b => b.BookId == bookDto.BookId);
            if (existingBook != null)
            {
                existingBook.Title = bookDto.Title;
                existingBook.Genre = bookDto.Genre;
                existingBook.PublishDate = bookDto.PublishDate;
                existingBook.AuthorId = bookDto.AuthorId;

                _unitOfWork.Books.Update(existingBook);
                _unitOfWork.Save();
            }
        }

        public void DeleteBook(int id)
        {
            _unitOfWork.Books.Delete(id);
            _unitOfWork.Save();
        }
    }
}
