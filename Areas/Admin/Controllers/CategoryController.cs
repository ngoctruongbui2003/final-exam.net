using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Repositories;
using shoes_final_exam.Repositories.Implement;

namespace shoes_final_exam.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/category")]
    public class CategoryController : Controller
	{
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;

        }

        [HttpGet("")]
        public IActionResult Index()
		{
			var list = _categoryRepository.GetAll();


            return View(list);
		}

        public IActionResult Add()
        {
            return View();
        }
	}
}
