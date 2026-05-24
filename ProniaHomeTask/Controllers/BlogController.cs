using Microsoft.AspNetCore.Mvc;

namespace ProniaHomeTask.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
