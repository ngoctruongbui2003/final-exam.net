using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Repositories;
using shoes_final_exam.Repositories.Implement;

namespace shoes_final_exam.Controllers
{
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

        public async Task<IActionResult> Detail(int id)
        {
            var productById = await _productRepository.GetById(id);

            if (productById == null)
            {
                return RedirectToAction("Index");
            }

            return View(productById);
        }

        public string GetFilteredProducts()
        {
            return "";
        }
    }
}
