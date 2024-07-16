using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using Repository;
using Service.Interfaces;
using Service;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Domain.Interfaces;

namespace Book.App_Start
{
        public static class UnityConfig
        {
        public static object Container { get; internal set; }

        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register all your components with the container here
            container.RegisterType<IAuthorService, AuthorService>();
            container.RegisterType<IBookService, BookService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IAuthorRepository, AuthorRepository>();
            container.RegisterType<IBookRepository, BookRepository>();

            container.RegisterType<LibraryContext>(new PerRequestLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
   }

