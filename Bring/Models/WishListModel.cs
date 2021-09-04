using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class WishListModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public string ImageName { get; set; }
        public string ImageContent { get; set; }
        public byte[] ImageData { get; set; }

    }
}