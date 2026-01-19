using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OWP.Models
{
    public class CartItem
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}