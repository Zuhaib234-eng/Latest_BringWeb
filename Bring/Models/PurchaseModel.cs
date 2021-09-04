using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string PurchaseName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}