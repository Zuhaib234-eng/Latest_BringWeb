using System.Web.Mvc;

namespace Bring.Controllers
{
    [HandleError]
    public class SingleProductController : Controller
    {
        // GET: SingleProduct
        public ActionResult Index(int id)
        {
            return View();
        }
    }
}