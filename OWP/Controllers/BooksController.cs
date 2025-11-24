using OWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWP.Controllers
{
    public class BooksController : Controller
    {

        private readonly ApplicationDbContext _context;
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        //POST: Books/Create
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }

            return View(book);
        }
    }
}