using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web
{
    public class SubCategoryVal
    {
        [Required(ErrorMessage="Please enter a subcategory")]
        [Display(Name = "SubCategory Name")]
        public string SubCategoryName { get; set; }
        [Display(Name="Category Name")]
        public string CategoryName { get; set; }
        [Display(Name="Active")]
        public bool IsActive { get; set; }
    }
    [MetadataType(typeof(SubCategoryVal))]
    public partial class SubCategory
    { }
}