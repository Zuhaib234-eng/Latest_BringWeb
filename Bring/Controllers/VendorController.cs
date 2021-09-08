using Bring.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Bring.Controllers
{
    [HandleError]
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult ProductDetail()
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product/GetVendorId/" + Session["LoginUser"].ToString()).Result;
            IEnumerable<Product> products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            return View(products);
        }

        public ActionResult OrderDetails()
        {
            IEnumerable<MonthlyReport> reports = null;
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("MonthlyReport/" + Session["LoginUser"].ToString()).Result;
            reports = response.Content.ReadAsAsync<IEnumerable<MonthlyReport>>().Result;
            return View(reports);
        }

        public ActionResult ProductUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProductUpload(Product product, HttpPostedFileBase file, HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            Product newObj = new Product();
            newObj.ProductName = product.ProductName;
            newObj.Price = product.Price;
            newObj.PhoneNo = product.PhoneNo;
            newObj.Email = product.Email;
            newObj.Description = product.Description;
            newObj.CategoryName = product.CategoryName;
            newObj.ProductStatus = "Normal";
            newObj.ProductStock = product.ProductStock;
            newObj.VendorId = Convert.ToInt32(Session["LoginUser"]);
            string filePath = Server.MapPath("../ProductImages/");
            string fileName = "";

            string NewGuid = Guid.NewGuid().ToString();
            //Image 1 
            fileName = Path.GetFileName(NewGuid + file.FileName);
            file.SaveAs(filePath + fileName);
            newObj.ImagePath1 = fileName;

            //Image 2
            fileName = Path.GetFileName(NewGuid + file2.FileName);
            file.SaveAs(filePath + fileName);
            newObj.ImagePath2 = fileName;

            //Image 3
            fileName = Path.GetFileName(NewGuid + file3.FileName);
            file.SaveAs(filePath + fileName);
            newObj.ImagePath3 = fileName;
            HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Product", newObj).Result;
            ViewBag.msg = "success";
            return View();
        }

        public ActionResult MonthlySummary()
        {
            IEnumerable<MonthlyReport> Mon = null;
            HttpResponseMessage Monresponse = GlobalVariable.WebApiClient.GetAsync("MonthlyReport/GetByVendor/" + Session["LoginUser"]).Result;
            Mon = Monresponse.Content.ReadAsAsync<IEnumerable<MonthlyReport>>().Result;
            decimal total = 0;
            List<MonthlyReport> li = Mon.ToList();
            for (int i = 0; i < Mon.Count(); i++)
            {
                total += li[i].Amount;
            }
            ViewBag.totalSales = total;

            return View(Mon);
        }
        [HttpPost]
        public ActionResult SetSuccessEmpty()
        {
            ViewBag.msg = "";
            return Json("");
        }
        [HttpPost]
        public ActionResult MonthlySummary(FormCollection form)
        {
            int Id = Convert.ToInt32(Session["LoginUser"]);
            string Month = form["Month"].ToString();
            HttpResponseMessage Monresponse = GlobalVariable.WebApiClient.GetAsync("MonthlyReport/GetByBoth/" + Id + "/" + Month).Result;
            IEnumerable<MonthlyReport> Mon = Monresponse.Content.ReadAsAsync<IEnumerable<MonthlyReport>>().Result;
            decimal total = 0;
            List<MonthlyReport> li = Mon.ToList();
            for (int i = 0; i < Mon.Count(); i++)
            {
                total += li[i].Amount;
            }
            ViewBag.totalSales = total;
            return View(Mon);
        }

        public ActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetProfile()
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Vendor/" + Session["LoginUser"].ToString()).Result;
            var data = response.Content.ReadAsAsync<VendorModel>().Result;
            return Json(new { status = "success", Vendor = data }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult EditProfile(VendorModel vendor)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Vendor/" + Session["LoginUser"].ToString(), vendor).Result;
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteProduct(int Id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("Product/" + Id.ToString()).Result;
            return Json("success");
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product/" + id.ToString()).Result;
            var data = response.Content.ReadAsAsync<Product>().Result;
            return View(data);
        }
        [HttpPost]
        public ActionResult EditProduct(Product pro)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Product/" + pro.Id.ToString(), pro).Result;
            ViewBag.msg = "success";
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("LoginUser");
            return RedirectToAction("Index", "Index");
        }
    }
}