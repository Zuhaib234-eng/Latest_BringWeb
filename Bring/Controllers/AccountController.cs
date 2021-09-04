using System.Net.Http;
using System.Web.Mvc;
using Bring.Models;
using System;
namespace Bring.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["LoginUser"]) > 0)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("User/" + Session["LoginUser"]).Result;
                var data = response.Content.ReadAsAsync<UserModel>().Result;
                return View(data);
            }
            else
            {
                @Session["msg"] = "success";
            }
            return RedirectToAction("Index","Index");
        }

        [HttpPost]
        public ActionResult Index(UserModel user)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("User/" + Session["LoginUser"], user).Result;
                return RedirectToAction("Index", "Index");
            }
            HttpResponseMessage dataResponse = GlobalVariable.WebApiClient.GetAsync("User/" + Session["LoginUser"]).Result;
            var data = dataResponse.Content.ReadAsAsync<UserModel>().Result;
            return View(data);                                 
        }
    }
}