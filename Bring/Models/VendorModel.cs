using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class VendorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Cnic { get; set; }
        public string Address { get; set; }
        public bool AdminLock { get; set; }
       
    }
}