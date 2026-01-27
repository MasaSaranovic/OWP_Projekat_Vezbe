using OWP.Helpers;
using OWP.Models;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OWP.Controllers
{
    public class CheckoutController : Controller
    {
        public ActionResult Index()
        {
            var cart = CartSession.GetCart();
            if (!cart.Any()) return RedirectToAction("Index", "Cart");

            //ToDo za popunjavanje emaila
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStripeSession(CheckoutViewModel model)
        {
            var cart = CartSession.GetCart();
            if (!cart.Any()) return RedirectToAction("Index", "Cart");

            if (!ModelState.IsValid) return View("Index", model);

            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];

            var lineItems = cart.Select(i => new SessionLineItemOptions
            {
                Quantity = i.Quantity,
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "rsd",
                    UnitAmount = (long)Math.Round(i.UnitPrice * 100m),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = i.Title
                    }
                }
            }).ToList();

            var domain = $"{Request.Url.Scheme}://{Request.Url.Authority}";

            var options = new SessionCreateOptions
            {
                Mode = "payment",
                SuccessUrl = domain + Url.Action("Success", "Checkout") + "?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + Url.Action("Cancel", "Checkout"),
                LineItems = lineItems,

                CustomerEmail = model.Email,

                Metadata = new System.Collections.Generic.Dictionary<string, string>
                {
                    ["fullName"] = model.FullName,
                    ["phone"] = model.Phone,
                    ["address"] = model.Address,
                    ["city"] = model.City,
                    ["postalCode"] = model.PostalCode,
                    ["country"] = model.Country,
                    ["note"] = model.Note ?? ""
                }
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);

        }

        public ActionResult Success(string session_id)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            var service = new SessionService();
            var session = service.Get(session_id);

            if(session.PaymentStatus == "paid")
            {
                CartSession.Clear();
                return View();
            }

            return RedirectToAction("Cancel");
        }
        
        public ActionResult Cancel()
        {
            return View();
        }
    }
}