using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OWP.Models
{
    public class CheckoutViewModel
    {
        [Required] public string FullName { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string Phone { get; set; }

        [Required] public string Address { get; set; }
        [Required] public string City { get; set; }
        [Required] public string PostalCode { get; set; }
        [Required] public string Country { get; set; } = "RS";

        public string Note { get; set; }

    }
}