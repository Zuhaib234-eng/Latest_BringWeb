using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class MonthlyReport
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public string Address { get; set; }
        public int Quantity { get; set; }
        
    }
}