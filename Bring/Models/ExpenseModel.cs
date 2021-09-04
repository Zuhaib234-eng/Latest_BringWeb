using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public string ExpenseType { get; set; }
        public string ExpenseName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}