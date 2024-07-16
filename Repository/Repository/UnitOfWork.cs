using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryContext _context;
        private IAuthorRepository _authors;
        private IBookRepository _books;

        public UnitOfWork(LibraryContext context)
        {
            _context = context;
        }

        public IAuthorRepository Authors
        {
            get
            {
                return _authors ?? (_authors = new AuthorRepository(_context));
            }
        }

        public IBookRepository Books
        {
            get
            {
                return _books ?? (_books = new BookRepository(_context));
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}