using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class ProductDetailsAdmin
    {
        public VendorModel Vendor { get; set; }
        public List<Product> Product { get; set; }
        //public CategoryModel Categories { get; set; }
    }
}