using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web
{
    public partial class Product
    {
        public string CreatedByUser
        {
            get;
            set;
        }
        public string UpdatedByUser
        {
            get;
            set;
        }
        public string CategoryName
        {
            get;
            set;
        }
        public string SubCategoryName
        {
            get;
            set;
        }
        public string ImageName
        {
            get;
            set;
        }
        public string ImagePath
        {
            get;
            set;
        }
    }
}