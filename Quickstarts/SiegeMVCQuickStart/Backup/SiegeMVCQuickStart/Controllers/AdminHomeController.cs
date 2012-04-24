using System.Web.Mvc;
using SiegeMVCQuickStart.SampleClasses;

namespace SiegeMVCQuickStart.Controllers
{
    public class AdminHomeController : HomeController
    {
        private readonly IAdminService adminService;

        public AdminHomeController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public ViewResult Users()
        {
            return View(adminService.GetUsers());
        }
    }
}
