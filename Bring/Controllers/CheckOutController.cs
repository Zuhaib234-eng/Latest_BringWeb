using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Bring.Models;

namespace Bring.Controllers
{
    [HandleError]
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public ActionResult Index()
        {


            ////int id = 2;
            //decimal totalPrice = 0;
            ////Session["LoginUser"] = id;
            IEnumerable<ShoppingCartModel> cartList = new List<ShoppingCartModel>();
            //HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("ShoppingCart/" + Session["LoginUser"].ToString()).Result;
            //cartList = response.Content.ReadAsAsync<IEnumerable<ShoppingCartModel>>().Result;
            //OrdersAndCartModel model = new OrdersAndCartModel();
            //model.Cart = cartList.ToList();
            //for (int i = 0; i < cartList.Count(); i++)
            //{
            //    totalPrice += Convert.ToDecimal(model.Cart[i].TotalPrice);
            //}
            OrdersAndCartModel model = new OrdersAndCartModel();
            //ViewBag.totalPrice = totalPrice;
            return View(model);
        }



        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckCart()
        {
            try
            {
                string Message = "";
                if (Session["CartProduct"] != null)
                {
                    Message = "success";
                }
                else
                {
                    Message = "fail";
                }
                return Json(new { status = "success", Message = Message, JsonRequestBehavior.AllowGet });
            }
            catch(Exception ex)
            {
                return Json(new { status = "success", Message = ex.Message, JsonRequestBehavior.AllowGet });
            }
        }
        [HttpPost]
        public ActionResult GetUser()
        {
            if (Session["LoginUser"] != null)
            {
                int UserId = Convert.ToInt32(Session["LoginUser"]);
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("User/" + UserId).Result;
                var data = response.Content.ReadAsAsync<UserModel>().Result;
                return Json(new { status = "success", data = data, JsonRequestBehavior.AllowGet });
            }
            return Json(new { status = "not found", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult Index(string createAnaccount, OrdersAndCartModel ordersAndCart)
        {
            
            decimal totalPrice = 0;
            string productId = null;
            string vendorId = null;
            string productName = null;
            string price = null;
            IEnumerable<ShoppingCartModel> cartList = null;
            HttpResponseMessage cartResponse = GlobalVariable.WebApiClient.GetAsync("ShoppingCart/" + Session["LoginUser"].ToString()).Result;
            cartList = cartResponse.Content.ReadAsAsync<IEnumerable<ShoppingCartModel>>().Result;

            OrdersAndCartModel model = new OrdersAndCartModel();
            model.Cart = cartList.ToList();
            for (int i = 0; i < cartList.Count(); i++)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product/" + model.Cart[i].ProductId.ToString()).Result;
                var data = response.Content.ReadAsAsync<Product>().Result;
                data.ProductStock = data.ProductStock - model.Cart[i].Quantity;
                HttpResponseMessage update = GlobalVariable.WebApiClient.PutAsJsonAsync("Product/" + model.Cart[i].ProductId.ToString(),data).Result;
                productId += Convert.ToString(model.Cart[i].ProductId) + ","; 
                vendorId += Convert.ToString(model.Cart[i].VendorId) + ",";
                productName += model.Cart[i].ProductName + ",";
                price += Convert.ToString(model.Cart[i].Price) + ",";
                totalPrice += Convert.ToDecimal(model.Cart[i].TotalPrice);
            }

            MonthlyReport report = new MonthlyReport();
            for (int i = 0; i < cartList.Count(); i++)
            {
                report.VendorId = model.Cart[i].VendorId;
                report.Month = DateTime.Now.ToString("MMMM");
                report.ProductName = model.Cart[i].ProductName;
                report.Date = DateTime.Now;
                report.Address = ordersAndCart.Orders.StreetAdress;
                report.Quantity = model.Cart[i].Quantity;
                report.Amount = model.Cart[i].TotalPrice;
                

                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("MonthlyReport", report).Result;
            }

            //ViewBag.totalPrice = totalPrice;
            //ViewBag.productId = productId.ToString();
            //ViewBag.vendorId = vendorId.ToString();
            //ViewBag.productName = productName.ToString();
            //ViewBag.price = price.ToString();

            if (createAnaccount != "true")
            {


                OrdersModel inOrders = new OrdersModel();
                inOrders.FirstName = ordersAndCart.Orders.FirstName;
                inOrders.LastName = ordersAndCart.Orders.LastName;
                inOrders.PhoneNo = ordersAndCart.Orders.PhoneNo;
                inOrders.PostCode = ordersAndCart.Orders.PostCode;
                inOrders.ShoppingCartId = 0;
                inOrders.State = ordersAndCart.Orders.State;
                inOrders.City = ordersAndCart.Orders.City;
                inOrders.StreetAdress = ordersAndCart.Orders.StreetAdress;
                inOrders.EmailAddress = ordersAndCart.Orders.EmailAddress;
                inOrders.UserId = Convert.ToInt32(Session["LoginUser"]);
                inOrders.PaymentMethod = ordersAndCart.Orders.PaymentMethod;
                inOrders.Date = DateTime.Now;
                inOrders.CouponCode = "";
                inOrders.Status = "OrderPlaced";
                inOrders.VendorId = vendorId;
                inOrders.ProductId = productId;
                inOrders.ProductName = productName;
                inOrders.Price = price;
                inOrders.TotalPrice = totalPrice;


                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Orders", inOrders).Result;
                return RedirectToAction("Index", "Index");


            }
            else
            {
                //int id = 1;

                //Session["LoginUser"] = id;

                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("User/" + Session["LoginUser"].ToString()).Result;
                var data = response.Content.ReadAsAsync<UserModel>().Result;

                OrdersModel inOrders = new OrdersModel();
                inOrders.FirstName = data.FirstName;
                inOrders.LastName = data.LastName;
                inOrders.PhoneNo = data.PhoneNo;
                inOrders.PostCode = data.PostCode;
                inOrders.State = data.State;
                inOrders.City = data.City;
                inOrders.Status = "OrderPlaced";
                inOrders.EmailAddress = data.Email;
                inOrders.StreetAdress = data.Address;
                inOrders.Country = data.Country;
                inOrders.Date = DateTime.Now;
                inOrders.UserId = data.Id;
                inOrders.EmailAddress = data.Email;
                inOrders.VendorId = vendorId;
                inOrders.ProductId = productId;
                inOrders.ProductName = productName;
                inOrders.Price = price;
                inOrders.TotalPrice = totalPrice;

                HttpResponseMessage ordersResponse = GlobalVariable.WebApiClient.PostAsJsonAsync("Orders", inOrders).Result;

                return RedirectToAction("Index", "Index");

            }
        }
    }

}