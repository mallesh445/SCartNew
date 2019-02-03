using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web
{
    public partial class Category
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
        public int ImageId
        {
            get;
            set;
        }
    }
}