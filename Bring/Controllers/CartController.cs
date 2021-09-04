using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Bring.Models;

namespace Bring.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            List<ShoppingCartModel> shoppingCartModelList = new List<ShoppingCartModel>();
            if (Session["CartProduct"] != null)
            {
                List<Product> CartProducts = Session["CartProduct"] as List<Product>;                
                int no = 0;
                for (int i = 0; i < CartProducts.Count; i++)
                {
                    no++;
                    ShoppingCartModel sshoppingCartModel = new ShoppingCartModel();
                    sshoppingCartModel.Id = no;
                    sshoppingCartModel.Image = CartProducts[i].ImagePath1;
                    sshoppingCartModel.Price = Convert.ToDecimal(CartProducts[i].Price);
                    sshoppingCartModel.ProductId = CartProducts[i].Id;
                    sshoppingCartModel.ProductName = CartProducts[i].ProductName;
                    sshoppingCartModel.Quantity = Convert.ToInt32(CartProducts[i].ProductStock);
                    shoppingCartModelList.Add(sshoppingCartModel);
                }
            }
            else
            {
                ViewBag.msg = "Cart is empty";
            }

            return View(shoppingCartModelList);
        }
        public class CartModel {
            public int ProductCounter { get; set; }
            public string Message { get; set; }
        }
        [HttpPost]
        public ActionResult AddToCart(int ProductId)
        {
            try
            {
                HttpResponseMessage Response = GlobalVariable.WebApiClient.GetAsync("Product/" + ProductId.ToString()).Result;
                var Product = Response.Content.ReadAsAsync<Product>().Result;
                if (Session["CartProduct"] == null)
                {
                    List<Product> ProductsList = new List<Product>();
                    Product.ProductStock = 1;
                    ProductsList.Add(Product);
                    Session["CartProduct"] = ProductsList;
                    CartModel cartModel = new CartModel() {
                         Message = "success", ProductCounter = 1
                    };
                    Session["ProductCounter"] = cartModel;
                }
                else
                {
                    List<Product> ProductList = Session["CartProduct"] as List<Product>;
                    var getProducts = ProductList.Where(s => s.Id == Product.Id).FirstOrDefault();
                    if (getProducts == null)
                    {
                        Product.ProductStock = 1;
                        ProductList.Add(Product);
                        Session["CartProduct"] = ProductList;
                        var GetStatus = Session["ProductCounter"] as CartModel;
                        GetStatus.ProductCounter++;
                        GetStatus.Message = "success";
                        Session["ProductCounter"] = GetStatus;
                    }                    
                    else
                    {
                        var getProduct = ProductList.Where(s => s.Id == ProductId).FirstOrDefault();
                        getProduct.ProductStock++;

                        Session["CartProduct"] = ProductList;
                        var GetStatus = Session["ProductCounter"] as CartModel;
                        GetStatus.ProductCounter++;
                        GetStatus.Message = "success";
                        Session["ProductCounter"] = GetStatus;
                    }
                }
                var GetMessage = Session["ProductCounter"] as CartModel;
                return Json(new { status = "success", GetMessage = GetMessage, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { status = "failure", error = ex.Message, JsonRequestBehavior.AllowGet });
            }

        }

        public ActionResult Remove(int id)
        {
            List<Product> ProductList = Session["CartProduct"] as List<Product>;
            var Product = ProductList.Where(s => s.Id == id).FirstOrDefault();
            ProductList.Remove(Product);
            Session["CartProduct"] = ProductList;
            return RedirectToAction("Index", "Cart");/*View(cartLists);*/
        }
        [HttpPost]
        public ActionResult UpdateCart(List<Product> products)
        {
            if (products.Count > 0)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    int productId = products[i].Id;
                    HttpResponseMessage Response = GlobalVariable.WebApiClient.GetAsync("Product/" + productId.ToString()).Result;
                    var Product = Response.Content.ReadAsAsync<Product>().Result;
                    Product.ProductStock = products[i].ProductStock;
                    products[i] = Product;

                }
                Session["CartProduct"] = products;
            }
            return Json(new { status = "success", JsonRequestBehavior.AllowGet });
        }


    }
}