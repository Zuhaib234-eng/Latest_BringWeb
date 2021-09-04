using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class ShopProduct
    {
       public List<Product> product { get; set; }
       public List<Product> latestProduct { get; set; }
    }
}