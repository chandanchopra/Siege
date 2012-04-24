using System.Web.Mvc;
using SiegeMVCQuickStart.SampleClasses;

namespace SiegeMVCQuickStart.Controllers
{
    public class UserHomeController : HomeController
    {
        private readonly IAccountService service;

        public UserHomeController(IAccountService service)
        {
            this.service = service;
        }

        public override ActionResult Index()
        {
            ViewData["Message"] = service.GetAccountDetails();

            return View();
        }
    }
}