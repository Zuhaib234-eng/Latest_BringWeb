using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Bring.Models;

namespace Bring.Controllers
{

    public class ShopController : Controller
    {

        // GET: Shop
        public ActionResult Index(List<Product> productResponseList = null)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            productResponseList = response.Content.ReadAsAsync<List<Product>>().Result;
            ShopProduct shop = new ShopProduct();
            shop.product = productResponseList.ToList();
            shop.latestProduct = productResponseList.Take(4).ToList();
            return View(shop);
        }


        [HttpPost]

        public ActionResult Index(string SearchProduct, List<Product> productResponseList = null)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            productResponseList = response.Content.ReadAsAsync<List<Product>>().Result;
            ShopProduct shop = new ShopProduct();
            shop.product = productResponseList.Where(s=>s.ProductName.Equals(SearchProduct)).ToList();
            shop.latestProduct = productResponseList.Take(4).ToList();
            return View(shop);
        }
        public ActionResult SingleProduct(int id)
        {
            if (id >= 0)
            {
                Session["ProductId"] = id;
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product/" + id.ToString()).Result;
                var data = response.Content.ReadAsAsync<Product>().Result;
                IEnumerable<CommentsModel> comments = null;
                HttpResponseMessage commentsResponse = GlobalVariable.WebApiClient.GetAsync("Comment/" + id.ToString()).Result;
                comments = commentsResponse.Content.ReadAsAsync<IEnumerable<CommentsModel>>().Result;
                ProductReviews proRev = new ProductReviews();
                proRev.commentsList = comments;
                proRev.product = data;
                return View(proRev);
            }
            else
            {
                return RedirectToAction("Index", "Shop");
            }

        }
        [HttpPost]
        public ActionResult SingleProduct(ProductReviews reviews)
        {

            if (Session["LoginUser"] != null)
            {
                //int id = 1;
                //Session["LoginUser"] = id;
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("User/" + Session["LoginUser"].ToString()).Result;
                var data = response.Content.ReadAsAsync<UserModel>().Result;
                CommentsModel comments = new CommentsModel();
                comments.Date = DateTime.Now;
                comments.ProductId = Convert.ToInt32(Session["ProductId"]);
                comments.UserId = Convert.ToInt32(Session["LoginUser"]);
                comments.Comments = reviews.comments.Comments;
                comments.UserName = data.FirstName;
                comments.Rating = 0;

                HttpResponseMessage post = GlobalVariable.WebApiClient.PostAsJsonAsync("Comment", comments).Result;
                return RedirectToAction("SingleProduct");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }



        public ActionResult ToCart(string Quantity)
        {
            int quantityProduct = Convert.ToInt32(Quantity);
            if (quantityProduct == 0)
            {
                quantityProduct = 1;
            }
            if (Session["LoginUser"] != null)
            {
                var currentProduct = Session["ProductId"];
                int id = Convert.ToInt32(currentProduct);
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product/" + Session["ProductId"].ToString()).Result;
                var data = response.Content.ReadAsAsync<Product>().Result;

                ShoppingCartModel cartModel = new ShoppingCartModel();

                cartModel.ProductId = data.Id;
                cartModel.Price = Convert.ToInt32(data.Price);
                cartModel.TotalPrice = Convert.ToInt32(data.Price) * quantityProduct;
                cartModel.ProductName = data.ProductName;
                cartModel.Quantity = quantityProduct;
                cartModel.UserId = Convert.ToInt32(Session["LoginUser"]);
                //cartModel.Image = data.ImageContentType;
                //cartModel.VendorId = data.VendorId;


                HttpResponseMessage Cartresponse = GlobalVariable.WebApiClient.PostAsJsonAsync("ShoppingCart", cartModel).Result;
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }



        public ActionResult WishList(int id)
        {
            if (Session["LoginUser"] != null)
            {
                var getProducts = GlobalVariable.WebApiClient.GetAsync("Product").Result;
                List<Product> Products = getProducts.Content.ReadAsAsync<List<Product>>().Result;

                WishListModel wishList = new WishListModel();
                var Product = Products.Where(s => s.Id == id).FirstOrDefault();
                wishList.ProductId = Product.Id;
                wishList.ProductName = Product.ProductName;
                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("WishList", wishList).Result;
                //HttpResponseMessage productResponse = GlobalVariable.WebApiClient.GetAsync("Product/" + id.ToString()).Result;
                //var productData = productResponse.Content.ReadAsAsync<Product>().Result;
                //WishListModel wishList = new WishListModel();
                //wishList.ProductId = productData.Id;
                //wishList.ProductName = productData.ProductName;
                ////HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("WishList", wishList).Result;

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }



        [HttpPost]
        public ActionResult RangeSlider(string ViewMin, string ViewMax)
        {
            var getProducts = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            List<Product> featured = getProducts.Content.ReadAsAsync<List<Product>>().Result;
            ShopProduct shop = new ShopProduct();
            shop.product = featured;
            shop.latestProduct = featured.Where(s => s.Price >= Convert.ToDecimal(ViewMin) && s.Price <= Convert.ToDecimal(ViewMax)).ToList();
            return View(shop);
            //IEnumerable<Product> latestResponseList = null;
            //HttpResponseMessage latestresponse = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            //latestResponseList = latestresponse.Content.ReadAsAsync<IEnumerable<Product>>().Result;

            //IEnumerable<Product> productList = null;
            //HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product/GetPrice/" + ViewMin + "/" + ViewMax).Result;
            //productList = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;



            //ShopProduct shop = new ShopProduct();
            //shop.product = productList.ToList();
            //shop.latestProduct = latestResponseList.Take(4).ToList();
            //return View(shop);

        }

        public ActionResult Category(string param)
        {
            var getProducts = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            List<Product> featured = getProducts.Content.ReadAsAsync<List<Product>>().Result;
            ShopProduct shop = new ShopProduct();
            shop.product = featured;
            shop.latestProduct = featured.Where(s => s.Category.Equals(param)).ToList();
            return View(shop);
            //IEnumerable<Product> latestResponseList = null;
            //HttpResponseMessage latestresponse = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            //latestResponseList = latestresponse.Content.ReadAsAsync<IEnumerable<Product>>().Result;

            //IEnumerable<Product> categoryResponseList = null;
            //HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product/GetCategoryName/" + param.ToString()).Result;
            //categoryResponseList = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;

            //ShopProduct shop = new ShopProduct();
            //shop.product = categoryResponseList.ToList();
            //shop.latestProduct = latestResponseList.Take(4).ToList();
            //return View(shop);
        }

        public ActionResult MainCategory(string param)
        {
            var getProducts = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            List<Product> featured = getProducts.Content.ReadAsAsync<List<Product>>().Result;
            ShopProduct shop = new ShopProduct();
            shop.product = featured;
            shop.latestProduct = featured.Where(s => s.Category.Equals(param)).ToList();
            return View(shop);
        }
    }
}