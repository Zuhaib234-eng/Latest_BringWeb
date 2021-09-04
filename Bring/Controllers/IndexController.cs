using Bring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace Bring.Controllers
{

    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index(IndexProduct newList=null)
        {          
            var getProducts = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            List<Product> featured = getProducts.Content.ReadAsAsync<List<Product>>().Result;
            newList.Shuffleproducts = featured.OrderBy(x => Guid.NewGuid()).ToList();
            newList.LatestProduct = featured.Where(mod => mod.ProductStatus.Equals("Featured")).Take(5).ToList();
            newList.SuperMart = featured.Where(s => s.Category.Equals("SuperMarket")).Take(4).ToList();                         
            newList.Vegetable = featured.Where(s => s.Category.Equals("FreshFruitsAndVegetable")).Take(4).ToList();            
            newList.Bakeries = featured.Where(s => s.Category.Equals("Bakeries")).Take(4).ToList();            
            newList.Others = featured.Where(s => s.Category.Equals("Others")).Take(4).ToList();
            newList.Cosmetic = featured.Where(s => s.Category == "CosmeticAndBeauty").Take(4).ToList();                      
            newList.Organic = featured.Where(s => s.Category.Equals("OrganicShop")).Take(4).ToList();
            return View(newList);
        }

    }
}