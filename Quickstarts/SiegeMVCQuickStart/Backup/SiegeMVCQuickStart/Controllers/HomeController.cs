using System.Web.Mvc;

namespace SiegeMVCQuickStart.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
