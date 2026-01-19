using OWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OWP.Helpers
{
    public static class CartSession
    {
        private const string Key = "CART";

        public static List<CartItem> GetCart()
        {
            var cart = HttpContext.Current.Session[Key] as List<CartItem>;
            if(cart == null)
            {
                cart = new List<CartItem>();
                HttpContext.Current.Session[Key] = cart;
            }

            return cart;
        }

        public static void Clear() => HttpContext.Current.Session.Remove(Key);
    }
}