using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using shoes_final_exam.Models;
using shoes_final_exam.Repositories;
using shoes_final_exam.Repositories.Implement;
using System.Data;

namespace shoes_final_exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly ISizeRepository _sizeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ISizeRepository sizeRepository,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _sizeRepository = sizeRepository;
            _webHostEnvironment = webHostEnvironment;

        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAll();

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "Id", "Name");
            ViewBag.Sizes = new SelectList(await _sizeRepository.GetAll(), "Id", "SizeNumber");
			return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAll(), "Id", "Name", product.CategoryId);
            ViewBag.Sizes = new SelectList(await _sizeRepository.GetAll(), "Id", "SizeNumber", product.SizeId);

            if (!ModelState.IsValid && product.Image != null)
            {
                return View();
            }

            if (product.ImageUpload != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;
                string filePath = Path.Combine(uploadDir, imageName);

                FileStream fs = new FileStream(filePath, FileMode.Create);
                await product.ImageUpload.CopyToAsync(fs);
                fs.Close();
                product.Image = imageName;
            }

            await _productRepository.Add(product);
            TempData["Success"] = "Thêm sản phẩm thành công";

            return RedirectToAction(nameof(ProductController.Index), "Product");
		}
    }
}
