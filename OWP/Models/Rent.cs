using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OWP.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public DateTime DateOfRent { get; set; }
        public DateTime DateOfReturn { get; set; }

    }
}