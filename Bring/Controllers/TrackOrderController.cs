using Bring.Models;
using System.Net.Http;
using System.Web.Mvc;

namespace Bring.Controllers
{
    [HandleError]
    public class TrackOrderController : Controller
    {
        // GET: TrackOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int Id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Orders/"+Id.ToString()).Result;
            var data = response.Content.ReadAsAsync<OrdersModel>().Result;

            return PartialView("_OrderTrack",data);
            
         
        }
    }
}