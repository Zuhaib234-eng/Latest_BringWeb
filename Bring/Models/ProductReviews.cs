
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bring.Models
{
    public class ProductReviews
    {
        public Product product { get; set; }
        public CommentsModel comments { get; set; }
        public IEnumerable<CommentsModel> commentsList  { get; set; }
    }
}