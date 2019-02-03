using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web
{
    public partial class Order
    {
        public string UserName
        {
            get;
            set;
        }
        public int FKProductId
        {
            get;
            set;
        }
        public int Quantity
        {
            get;
            set;
        }
        public decimal Cost
        {
            get;
            set;
        }
        public int OrderId
        {
            get;
            set;
        }
    }
}