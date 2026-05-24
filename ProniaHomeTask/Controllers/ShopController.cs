using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaHomeTask.DAL.Context;
using ProniaHomeTask.Models;
using ProniaHomeTask.ViewModels;

namespace ProniaHomeTask.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int? id)
        {

            if (id == null) return BadRequest();


            Product product = await _context.Products
                .Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            DetailVM detailVM = new DetailVM
            {
                Product = product
            };

            return View(detailVM);
        }
    }
}
