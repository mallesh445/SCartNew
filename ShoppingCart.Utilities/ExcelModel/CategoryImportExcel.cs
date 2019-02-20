using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Utilities.ExcelModel
{
    public class CategoryImportExcel
    {
        public string CategoryName { get; set; }
        public string CreatedByUser { get; set; }
        public string UpdatedByUser { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Active { get; set; }
    }
}
