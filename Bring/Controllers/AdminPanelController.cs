using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Bring.Models;

namespace Bring.Controllers
{
    [HandleError]
    public class AdminPanelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        // GET: AdminPanel
        public ActionResult OrdersDetail()
        {

            IEnumerable<OrdersModel> ordersList = null;
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Orders").Result;
            ordersList = response.Content.ReadAsAsync<IEnumerable<OrdersModel>>().Result;
            return View(ordersList);

        }
        public ActionResult UserDetails()
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("User").Result;
            IEnumerable<UserModel> userList = response.Content.ReadAsAsync<IEnumerable<UserModel>>().Result;
            return View(userList);
        }
        public ActionResult ProductDetails()
        {
            HttpResponseMessage productResponse = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            IEnumerable<Product> productList = productResponse.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            return View(productList);
        }
        public ActionResult ViewProfile(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Vendor/" + id.ToString()).Result;
            var data = response.Content.ReadAsAsync<VendorModel>().Result;
            return View(data);
        }
        public ActionResult EditProfile(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Vendor/" + id.ToString()).Result;
            var data = response.Content.ReadAsAsync<VendorModel>().Result;
            return View(data);
        }
        public ActionResult VendorDetails()
        {            
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Vendor").Result;
            IEnumerable<VendorModel> vendorList = response.Content.ReadAsAsync<IEnumerable<VendorModel>>().Result;
            return View(vendorList);
        }

        [HttpGet]
        public ActionResult ProductStatus(int id)
        {
            HttpResponseMessage productResponse = GlobalVariable.WebApiClient.GetAsync("Product/" + id.ToString()).Result;
            var data = productResponse.Content.ReadAsAsync<Product>().Result;
            return PartialView("_ProductStatus", data);
        }
        [HttpPost]
        public ActionResult ProductStatus(Product pro)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Product/" + pro.Id, pro).Result;
            return RedirectToAction("ProductDetails");
        }     
        public ActionResult Status(int id)
        {
            HttpResponseMessage orderResponse = GlobalVariable.WebApiClient.GetAsync("Orders/" + id.ToString()).Result;
            var data = orderResponse.Content.ReadAsAsync<OrdersModel>().Result;
            return PartialView("_OrderStatus", data);
        }
        [HttpPost]
        public ActionResult Status(OrdersModel ord)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Orders/" + ord.Id.ToString(), ord).Result;
            return RedirectToAction("OrdersDetail");
        }

        public ActionResult Expense()
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Expense").Result;
            IEnumerable<ExpenseModel> expList = response.Content.ReadAsAsync<IEnumerable<ExpenseModel>>().Result;
            return View(expList);
        }

        [HttpGet]
        public ActionResult AddExpense()
        {
            return PartialView("_ExpensePartial");
        }

        [HttpGet]
        public ActionResult EditExpense(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Expense/" + id.ToString()).Result;
            var data = response.Content.ReadAsAsync<ExpenseModel>().Result;
            return PartialView("_ExpensePartial", data);
        }

        [HttpPost]
        public ActionResult EditExpense(ExpenseModel exp)
        {
            if (exp.Id == 0)
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Expense/AddExpense", exp).Result;
                return RedirectToAction("Expense");
            }

            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Expense/" + exp.Id.ToString(), exp).Result;
                return RedirectToAction("Expense");
            }
        }
        public ActionResult DeleteExpense(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("Expense/" + id.ToString()).Result;
            return RedirectToAction("Expense");

        }
        public ActionResult Purchase()
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Purchase").Result;
            IEnumerable<PurchaseModel> purchaseList = response.Content.ReadAsAsync<IEnumerable<PurchaseModel>>().Result;
            return View(purchaseList);
        }
        public ActionResult AddPurchase()
        {
            return PartialView("_PurchasePartial");
        }
        [HttpGet]
        public ActionResult EditPurchase(int Id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Purchase/" + Id.ToString()).Result;
            var data = response.Content.ReadAsAsync<PurchaseModel>().Result;
            return PartialView("_PurchasePartial", data);
        }
        [HttpPost]
        public ActionResult EditPurchase(PurchaseModel chase)
        {
            if (chase.Id == 0)
            {
                HttpResponseMessage Addresponse = GlobalVariable.WebApiClient.PostAsJsonAsync("Purchase", chase).Result;
                return RedirectToAction("Purchase");
            }
            else
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Purchase/" + chase.Id.ToString(), chase).Result;
                return RedirectToAction("Purchase");
            }
        }

        public ActionResult DeletePurchase(int Id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("Purchase/" + Id.ToString()).Result;
            return RedirectToAction("Purchase");
        }
        [HttpPost]
        public ActionResult ExpenseSearch(DateTime searchdate)
        {
            string NewDate = searchdate.ToString("yyyy-MM-dd");
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Expense/GetByDate/" + NewDate.ToString()).Result;
            IEnumerable<ExpenseModel> expList = response.Content.ReadAsAsync<IEnumerable<ExpenseModel>>().Result;
            return View(expList);
        }

        public ActionResult PurchaseSearch(DateTime searchdate)
        {
            string NewDate = searchdate.ToString("yyyy-MM-dd");
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Purchase/GetByDate/" + NewDate.ToString()).Result;
            IEnumerable<PurchaseModel>  expList = response.Content.ReadAsAsync<IEnumerable<PurchaseModel>>().Result;
            return View(expList);
        }
        public ActionResult Sales()
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("MonthlyReport").Result;
            IEnumerable<MonthlyReport> mr = response.Content.ReadAsAsync<IEnumerable<MonthlyReport>>().Result;
            decimal total = 0;
            List<MonthlyReport> li = mr.ToList();
            for (int i = 0; i < mr.Count(); i++)
            {
                total += li[i].Amount;
            }
            ViewBag.totalSales = total;
            return View(mr);
        }
        [HttpPost]
        public ActionResult SearchMonth(string Month, string vendorId)
        {
            if (vendorId != "" && Month != "")
            {                
                HttpResponseMessage Monresponse = GlobalVariable.WebApiClient.GetAsync("MonthlyReport/GetByBoth/" + vendorId + "/" + Month).Result;
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
            else if (vendorId == "" && Month != "")
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("MonthlyReport/GetByMonth/" + Month).Result;
                IEnumerable<MonthlyReport> mr = response.Content.ReadAsAsync<IEnumerable<MonthlyReport>>().Result;
                decimal total = 0;
                List<MonthlyReport> li = mr.ToList();
                for (int i = 0; i < mr.Count(); i++)
                {
                    total += li[i].Amount;
                }
                ViewBag.totalSales = total;
                return View(mr);
            }
            else if (vendorId != "" && Month == "")
            {
                HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("MonthlyReport/GetByVendor/" + vendorId).Result;
                IEnumerable<MonthlyReport> mr = response.Content.ReadAsAsync<IEnumerable<MonthlyReport>>().Result;
                decimal total = 0;
                List<MonthlyReport> li = mr.ToList();
                for (int i = 0; i < mr.Count(); i++)
                {
                    total += li[i].Amount;
                }
                ViewBag.totalSales = total;
                return View(mr);
            }
            else
            {
                return RedirectToAction("Sales");
            }
        }


        public ActionResult MonthlyReport(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("MonthlyReport/GetByVendor/" + id).Result;
            IEnumerable<MonthlyReport> mr = response.Content.ReadAsAsync<IEnumerable<MonthlyReport>>().Result;
            decimal total = 0;
            List<MonthlyReport> li = mr.ToList();
            for (int i = 0; i < mr.Count(); i++)
            {
                total += li[i].Amount;
            }
            ViewBag.totalSales = total;
            return View(mr);

        }

    }
}