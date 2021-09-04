using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Bring.Models;

namespace Bring.Controllers
{
    [HandleError]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string UserName, string Password)
        {
            IEnumerable<UserModel> userList = null;
            HttpResponseMessage userResponse = GlobalVariable.WebApiClient.GetAsync("User").Result;
            userList = userResponse.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
            IEnumerable<VendorModel> vendorList = null;
            HttpResponseMessage vendorResponse = GlobalVariable.WebApiClient.GetAsync("Vendor").Result;
            vendorList = vendorResponse.Content.ReadAsAsync<IEnumerable<VendorModel>>().Result;
            var user = userList.Where(s => s.Email == UserName && s.Password == Password).FirstOrDefault();
            if (user != null)
            {
                Session["LoginUser"] = user.Id;
                return RedirectToAction("Index", "Index");
            }
            else
            {
                ViewBag.Msg = "Fail";
            }
            var vendor = vendorList.Where(s => s.Email == UserName && s.Password == Password).FirstOrDefault();
            if (vendor != null)
            {
                Session["LoginUser"] = vendor.Id;
                return RedirectToAction("ProductDetail", "Vendor");
            }
            else
            {
                ViewBag.Msg = "Fail";
            }
            if (UserName == "Admin" && Password == "Admin123")
            {
                return RedirectToAction("OrdersDetail", "AdminPanel");
            }
            else
            {
                ViewBag.Msg = "Fail";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(string Email, string FirstName, string Password)
        {
            try
            {
                UserModel user = new UserModel();
                user.FirstName = FirstName;
                user.Email = Email;
                user.Country = string.Empty;
                user.City = string.Empty;
                user.Address = string.Empty;
                user.AdminLock = false;
                user.LastName = string.Empty;
                user.Password = Password;
                user.PhoneNo = string.Empty;
                user.PostCode = string.Empty;
                user.State = string.Empty;

                HttpResponseMessage userResponse = GlobalVariable.WebApiClient.GetAsync("User").Result;
                var data = userResponse.Content.ReadAsAsync<List<UserModel>>().Result;
                var getUser = data.Where(s => s.Email == Email).FirstOrDefault();
                string Message = "";
                if (getUser != null)
                {
                    if (getUser.Password == Password)
                    {
                        Session["LoginUser"] = getUser.Id;
                        Message = "success";
                        return Json(new { status = "success", Message = Message, JsonRequestBehavior.AllowGet });
                    }
                    else if (getUser.Email == Email)
                    {
                        Message = "Forget Password";                        
                        return Json(new { status = "success", Message = Message, JsonRequestBehavior.AllowGet });
                    }
                }                
                else
                {
                    HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("User", user).Result;
                    HttpResponseMessage usesrResponse = GlobalVariable.WebApiClient.GetAsync("User").Result;
                    var responsedata = usesrResponse.Content.ReadAsAsync<List<UserModel>>().Result;
                    var getNewUser = responsedata.Where(s => s.Email == Email).FirstOrDefault();
                    Session["LoginUser"] = getNewUser.Id;
                    Message = "success";
                }
                return Json(new { status = "success", Message = Message, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { status = "fail", data = ex.Message, JsonRequestBehavior.AllowGet });
            }
        }


    }
}