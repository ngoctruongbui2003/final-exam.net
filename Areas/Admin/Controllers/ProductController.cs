using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Repositories;

namespace shoes_final_exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAll();

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var products = await _productRepository.GetAll();

            return View(products);
        }
    }
}
