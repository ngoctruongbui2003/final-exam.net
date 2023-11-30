using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Models;
using shoes_final_exam.Models.ViewModels;
using shoes_final_exam.Repositories;

namespace shoes_final_exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository, ILogger<CategoryController> logger) 
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> CategoryList()
        {
            try
            {
                
                var categories = await _categoryRepository.GetAllReturnMV();

                return new JsonResult(Ok(categories));
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CategoryList.");
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        public async Task<JsonResult> Add([FromBody] string categoryName)
        {
            try
            {

                var categories = await _categoryRepository.Add(new CategoryVM()
                {
                    Name = categoryName
                });

                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CategoryList.");
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
	}
}
