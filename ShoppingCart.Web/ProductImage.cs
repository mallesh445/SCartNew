//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShoppingCart.Web
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductImage
    {
        public int PKImageId { get; set; }
        public int FKProductId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
