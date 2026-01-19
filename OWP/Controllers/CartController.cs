using OWP.Helpers;
using OWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWP.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var cart = CartSession.GetCart();
            return View(cart);
        }

        [HttpPost]
        public ActionResult Add(int bookId, int qty = 1)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null) return HttpNotFound();

            var cart = CartSession.GetCart();
            var existing = cart.FirstOrDefault(x => x.BookId == bookId);

            if (existing != null) existing.Quantity += qty;
            else cart.Add(new OWP.Models.CartItem
            {
                BookId = book.Id,
                Title = book.Name,
                UnitPrice = (decimal)book.Price,
                Quantity = qty
            });

            return RedirectToAction("Index");
          
        }

        [HttpPost]
        public ActionResult Remove(int bookId)
        {
            var cart = CartSession.GetCart();
            var item = cart.FirstOrDefault(x => x.BookId == bookId);
            if (item != null) cart.Remove(item);

            return RedirectToAction("Index");
        }
    }
}