using Microsoft.AspNetCore.Mvc;

namespace ProniaHomeTask.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
