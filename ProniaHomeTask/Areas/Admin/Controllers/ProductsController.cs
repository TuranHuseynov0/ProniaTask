using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProniaHomeTask.DAL.Context;
using ProniaHomeTask.Models;
using ProniaHomeTask.Services;

namespace ProniaHomeTask.Areas.Admin.Controllers
{
    public class ProductsController : AdminController
    {
        private readonly AppDbContext _context;
        private readonly IFileUploadService _fileUploadService;
        private const string DefaultImagePath = "uploads/products/no-image.svg";

        public ProductsController(AppDbContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ModelState.Remove(nameof(Product.ImageUpload));
            ModelState.Remove(nameof(Product.Category));

            if (ModelState.IsValid)
            {
                try
                {
                    product.ImgUrl = product.ImageUpload != null
                        ? await _fileUploadService.SaveImageAsync(product.ImageUpload)
                        : DefaultImagePath;

                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(nameof(Product.ImageUpload), ex.Message);
                }
            }

            await LoadCategoriesAsync(product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            await LoadCategoriesAsync(product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            ModelState.Remove(nameof(Product.ImageUpload));
            ModelState.Remove(nameof(Product.Category));

            var existing = await _context.Products.FindAsync(id);
            if (existing == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageUpload != null)
                    {
                        _fileUploadService.DeleteImage(existing.ImgUrl);
                        existing.ImgUrl = await _fileUploadService.SaveImageAsync(product.ImageUpload);
                    }

                    existing.Name = product.Name;
                    existing.Price = product.Price;
                    existing.Description = product.Description;
                    existing.CategoryId = product.CategoryId;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(nameof(Product.ImageUpload), ex.Message);
                }
            }

            product.ImgUrl = existing.ImgUrl;
            await LoadCategoriesAsync(product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _fileUploadService.DeleteImage(product.ImgUrl);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCategoriesAsync(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(
                await _context.Categories.OrderBy(c => c.Name).ToListAsync(),
                "Id",
                "Name",
                selectedId);
        }
    }
}
