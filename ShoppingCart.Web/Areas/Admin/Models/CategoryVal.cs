using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web
{
    public class CategoryVal
    {
        [Required(ErrorMessage="Please enter category name")]
        [Display(Name="Category Name")]
        public string CategoryName { get; set; }
        [Display(Name="Active")]
        public bool IsActive { get; set; }
    }
    [MetadataType(typeof(CategoryVal))]
    public partial class Category
    { }
}