using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class OrdersModel
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string CouponCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string StreetAdress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public string PaymentMethod { get; set; }
        public int UserId { get; set; }
        public string VendorId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string VendorName { get; set; }
        public string Price { get; set; }
        
       




    }
}