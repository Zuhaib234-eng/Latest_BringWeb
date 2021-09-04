using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class OrdersAndCartModel
    {
        public OrdersModel Orders { get; set; }
        public List<ShoppingCartModel> Cart { get; set; }

        public bool checkBox { get; set; }
    }
}