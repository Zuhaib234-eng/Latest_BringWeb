using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Bring.Models;

namespace Bring.Controllers
{
    public class VendorSignUpController : Controller
    {
        // GET: VendorSignUp
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(VendorModel vendor)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Vendor", vendor).Result;
            string result = await response.Content.ReadAsStringAsync();
            if (result.Contains("\"Data inserted\""))
            {
                ViewBag.msg = "success";
            }
            else
            {
                ViewBag.msg = "already exist";
            }
            return View();
        }
    }
}