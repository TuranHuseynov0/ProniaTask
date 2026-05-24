using Microsoft.AspNetCore.Mvc;

namespace ProniaHomeTask.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
