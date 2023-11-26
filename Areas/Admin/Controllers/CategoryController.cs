using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Repositories;

namespace shoes_final_exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _categoryRepository.GetAll();

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var list = await _categoryRepository.GetAll();

            return View(list);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }
    }
}
