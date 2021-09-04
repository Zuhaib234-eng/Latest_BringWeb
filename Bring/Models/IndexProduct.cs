using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class IndexProduct
    {
        public List<Product> products { get; set; }
        public List<Product> Shuffleproducts { get; set; }

        public List<Product> Bakeries { get; set; }
        public List<Product> LatestProduct { get; set; }
        public List<Product> Cosmetic { get; set; }
        public List<Product> Organic { get; set; }
        public List<Product> SuperMart { get; set; }
        public List<Product> Vegetable { get; set; }
        public List<Product> Others { get; set; }
    }
}