using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using Bring.Models;

namespace Bring.Controllers
{
    [HandleError]
    public class WishListController : Controller
    {
        // GET: WishList
        public ActionResult Index()
        {
            if (Session["LoginUser"] != null)
            {
                IEnumerable<WishListModel> wishLists = null;
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("WishList/" + Session["LoginUser"].ToString()).Result;
                wishLists = response.Content.ReadAsAsync<IEnumerable<WishListModel>>().Result;
                return View(wishLists);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        public ActionResult AddCart(int id)
        {
            if (Session["LoginUser"] != null)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product/" + id.ToString()).Result;
                var data = response.Content.ReadAsAsync<Product>().Result;

                ShoppingCartModel cartModel = new ShoppingCartModel();

                cartModel.ProductId = data.Id;
                cartModel.Price = Convert.ToInt32(data.Price);
                cartModel.TotalPrice = Convert.ToInt32(data.Price) * 1;
                cartModel.ProductName = data.ProductName;
                cartModel.Quantity = 1;
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

        public ActionResult Remove(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("WishList/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }





    }
}