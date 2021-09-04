using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class Sales
    {
        public List<MonthlyReport> report { get; set; }
        public List<VendorModel> vendor { get; set; }
    }
}