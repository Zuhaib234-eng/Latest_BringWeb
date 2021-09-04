using Bring.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System;
namespace Bring.Controllers
{
    public class MyOrderController : Controller
    {
        // GET: MyOrder
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["LoginUser"]) > 0)
            {
                IEnumerable<OrdersModel> ordersList = null;
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Orders/GetUser/" + Session["LoginUser"].ToString()).Result;
                ordersList = response.Content.ReadAsAsync<IEnumerable<OrdersModel>>().Result;

                return View(ordersList);
            }
            else
            {
                Session["msg"] = "success";
            }
            return RedirectToAction("Index", "Index");
            //return Json(new { status = "Failure"}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult resetSession()
        {
            Session.Remove("msg");
            return Json("",JsonRequestBehavior.AllowGet);
        }
    }
}