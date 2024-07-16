using Book.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.DTOs;
using Service.Interfaces;

namespace Book.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BookController(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        public ActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            var bookViewModels = books.Select(b => new BookViewModel
            {
                BookId = b.BookId,
                Title = b.Title,
                Genre = b.Genre,
                PublishDate = b.PublishDate,
                AuthorId = b.AuthorId,
                Author = b.Author != null ? new AuthorViewModel
                {
                    AuthorId = b.Author.AuthorId,
                    Name = b.Author.Name,
                    Age = b.Author.Age,
                    Country = b.Author.Country
                } : null 
            }).ToList();

            return View(bookViewModels);
        }

        public ActionResult Create()
        {
            ViewBag.Authors = new SelectList(_authorService.GetAllAuthors(), "AuthorId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                var bookDto = new BookDto
                {
                    Title = bookViewModel.Title,
                    Genre = bookViewModel.Genre,
                    PublishDate = bookViewModel.PublishDate,
                    AuthorId = bookViewModel.AuthorId
                };

                _bookService.AddBook(bookDto);
                return RedirectToAction("Index");
            }
            ViewBag.Authors = new SelectList(_authorService.GetAllAuthors(), "AuthorId", "Name", bookViewModel.AuthorId);
            return View(bookViewModel);
        }

        public ActionResult CombinedView()
        {
            var books = _bookService.GetAllBooks();
            var authors = _authorService.GetAllAuthors();

            var combinedViewModels = books.Select(b => new BookAuthorViewModel
            {
                BookId = b.BookId,
                Title = b.Title,
                Genre = b.Genre,
                PublishDate = b.PublishDate,
                AuthorId = b.AuthorId,
                AuthorName = b.Author?.Name,
                AuthorAge = b.Author?.Age ?? 0,
                AuthorCountry = b.Author?.Country
            }).ToList();

            return View(combinedViewModels);
        }
        public ActionResult Edit(int id)
        {
            var book = _bookService.GetAllBooks().FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var bookViewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                AuthorId = book.AuthorId
            };

            ViewBag.Authors = new SelectList(_authorService.GetAllAuthors(), "AuthorId", "Name", book.AuthorId);

            return View(bookViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                var bookDto = new BookDto
                {
                    BookId = bookViewModel.BookId,
                    Title = bookViewModel.Title,
                    Genre = bookViewModel.Genre,
                    PublishDate = bookViewModel.PublishDate,
                    AuthorId = bookViewModel.AuthorId
                };

                _bookService.UpdateBook(bookDto);
                return RedirectToAction("Index");
            }

            ViewBag.Authors = new SelectList(_authorService.GetAllAuthors(), "AuthorId", "Name", bookViewModel.AuthorId);
            return View(bookViewModel);
        }

        public ActionResult Delete(int id)
        {
            var book = _bookService.GetAllBooks().FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var bookViewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                AuthorId = book.AuthorId
            };

            return View(bookViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
