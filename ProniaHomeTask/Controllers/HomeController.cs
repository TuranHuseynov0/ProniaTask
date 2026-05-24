using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProniaHomeTask.DAL.Context;
using ProniaHomeTask.Models;
using ProniaHomeTask.ViewModels;

namespace ProniaHomeTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM
            {
                Products = _context.Products.ToList()
            };

            return View(vm);
        }
    }
}
