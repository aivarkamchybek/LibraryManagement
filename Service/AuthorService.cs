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
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _unitOfWork.Authors.GetAll();
            return authors.Select(a => new AuthorDto
            {
                AuthorId = a.AuthorId,
                Name = a.Name,
                Age = a.Age,
                Country = a.Country,
                Books = (a.Books ?? new List<Book>()).Select(b => new BookDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Genre = b.Genre,
                    PublishDate = b.PublishDate,
                    AuthorId = b.AuthorId
                }).ToList()
            });
        }


        public void AddAuthor(AuthorDto authorDto)
        {
            var author = new Author
            {
                Name = authorDto.Name,
                Age = authorDto.Age,
                Country = authorDto.Country,
                Books = authorDto.Books.Select(b => new Book
                {
                    Title = b.Title,
                    Genre = b.Genre,
                    PublishDate = b.PublishDate,
                    AuthorId = b.AuthorId
                }).ToList()
            };
            _unitOfWork.Authors.Insert(author);
            _unitOfWork.Save();
        }


        public void UpdateAuthor(AuthorDto authorDto)
        {
            var author = _unitOfWork.Authors.GetAll().FirstOrDefault(a => a.AuthorId == authorDto.AuthorId);
            if (author != null)
            {
                author.Name = authorDto.Name;
                author.Age = authorDto.Age;
                author.Country = authorDto.Country;
                author.Books = authorDto.Books.Select(b => new Book
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Genre = b.Genre,
                    PublishDate = b.PublishDate,
                    AuthorId = b.AuthorId
                }).ToList();

                _unitOfWork.Authors.Update(author);
                _unitOfWork.Save();
            }
        }

        public void DeleteAuthor(int id)
        {
            _unitOfWork.Authors.Delete(id);
            _unitOfWork.Save();
        }
    }
}
