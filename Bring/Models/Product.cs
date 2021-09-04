using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> VendorId { get; set; }
        public string CategoryName { get; set; }
        public string ProductStatus { get; set; }
        public Nullable<int> ProductStock { get; set; }
        public string Category { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public List<Product> Shuffle { get; set; }



    }
}