using Book.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.DTOs;
using Service.Interfaces;
using Service;
using Domain.Entities;

namespace Book.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public ActionResult Index()
        {
            var authors = _authorService.GetAllAuthors();
            var authorViewModels = authors.Select(a => new AuthorViewModel
            {
                AuthorId = a.AuthorId,
                Name = a.Name,
                Age = a.Age,
                Country = a.Country,
                Books = a.Books.Select(b => new BookViewModel
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Genre = b.Genre,
                    PublishDate = b.PublishDate,
                    AuthorId = b.AuthorId
                }).ToList()
            });

            return View(authorViewModels);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                var authorDto = new AuthorDto
                {
                    Name = authorViewModel.Name,
                    Age = authorViewModel.Age,
                    Country = authorViewModel.Country,
                    Books = (authorViewModel.Books ?? new List<BookViewModel>()).Select(b => new BookDto
                    {
                        Title = b.Title,
                        Genre = b.Genre,
                        PublishDate = b.PublishDate,
                        AuthorId = b.AuthorId
                    }).ToList()
                };

                _authorService.AddAuthor(authorDto);
                return RedirectToAction("Index");
            }
            return View(authorViewModel);
        }
        //new
        public ActionResult Edit(int id)
        {
            var author = _authorService.GetAllAuthors().FirstOrDefault(a => a.AuthorId == id);
            if (author == null)
            {
                return HttpNotFound();
            }

            var authorViewModel = new AuthorViewModel
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Age = author.Age,
                Country = author.Country,
                Books = author.Books.Select(b => new BookViewModel
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Genre = b.Genre,
                    PublishDate = b.PublishDate,
                    AuthorId = b.AuthorId
                }).ToList()
            };

            return View(authorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                var authorDto = new AuthorDto
                {
                    AuthorId = authorViewModel.AuthorId,
                    Name = authorViewModel.Name,
                    Age = authorViewModel.Age,
                    Country = authorViewModel.Country,
                    Books = (authorViewModel.Books ?? new List<BookViewModel>()).Select(b => new BookDto
                    {
                        BookId = b.BookId,
                        Title = b.Title,
                        Genre = b.Genre,
                        PublishDate = b.PublishDate,
                        AuthorId = b.AuthorId
                    }).ToList()
                };

                _authorService.UpdateAuthor(authorDto);
                return RedirectToAction("Index");
            }
            return View(authorViewModel);
        }

        public ActionResult Delete(int id)
        {
            var author = _authorService.GetAllAuthors().FirstOrDefault(a => a.AuthorId == id);
            if (author == null)
            {
                return HttpNotFound();
            }

            var authorViewModel = new AuthorViewModel
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Age = author.Age,
                Country = author.Country,
                Books = author.Books.Select(b => new BookViewModel
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Genre = b.Genre,
                    PublishDate = b.PublishDate,
                    AuthorId = b.AuthorId
                }).ToList()
            };

            return View(authorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _authorService.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
    }
}