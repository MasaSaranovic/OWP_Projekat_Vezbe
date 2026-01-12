using OWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OWP.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {

        public ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Books
        public ActionResult Index(string search)
        {
            var books = from b in _context.Books select b;

            if (!String.IsNullOrEmpty(search))
            {
                books = books.Where(b => b.Name.Contains(search));
            }
            
            return View(books.ToList());
        }

        public ActionResult Details(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null) return HttpNotFound();
            return View(book);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Book book = _context.Books.Find(id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(book).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
                
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Book book = _context.Books.Find(id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _context.Books.Find(id);
            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}