using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web
{
    public class BillingAddressVal
    {
        [Required]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
    }
    [MetadataType(typeof(BillingAddressVal))]
    public partial class BillingAddress
    {
    }
}