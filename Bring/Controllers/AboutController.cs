using System.Web.Mvc;

namespace Bring.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult  PrivacyPolicy()
        {
            return View();
        }

        public ActionResult ReturnPolicy()
        {
            return View();
        }
        public ActionResult Shipping()
        {
            return View();
        }

        public ActionResult TermsCondition()
        {
            return View();
        }
    }
}