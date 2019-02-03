using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web
{
    public class ProductVal
    {
        [Required(ErrorMessage = "Please enter product name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Please enter Descrption")]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
         [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
    [MetadataType(typeof(ProductVal))]
    public partial class Product
    {
    }
}