using Microsoft.AspNetCore.Mvc;

namespace ProniaHomeTask.Controllers
{
    public class _404Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
