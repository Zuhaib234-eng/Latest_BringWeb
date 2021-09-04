using Bring.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace Bring.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Category(string param)
        {
            HttpResponseMessage latestresponse = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            IEnumerable<Product> latestResponseList = latestresponse.Content.ReadAsAsync<IEnumerable<Product>>().Result;          
            ShopProduct shop = new ShopProduct();
            shop.product = latestResponseList.Where(s=>s.Category == param).ToList();
            shop.latestProduct = latestResponseList.Take(4).ToList();
            return View(shop);
        }
    }
}