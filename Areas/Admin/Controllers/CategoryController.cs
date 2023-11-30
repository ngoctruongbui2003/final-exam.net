using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpPost]
        public async Task<JsonResult> Delete([FromBody] string id)
        {
            try
            {

                var categories = await _categoryRepository.Delete(Int32.Parse(id));

                if (categories == false) return new JsonResult(NotFound());

                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CategoryList.");
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        public async Task<JsonResult> Detail([FromQuery] string id)
        {
            try
            {
                var foundedCategory = await _categoryRepository.GetByIdReturnMV(Int32.Parse(id));

                return new JsonResult(Ok(foundedCategory));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CategoryList.");
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        public async Task<JsonResult> Update([FromQuery] string id)
        {
            try
            {
                var foundedCategory = await _categoryRepository.GetByIdReturnMV(Int32.Parse(id));

                return new JsonResult(Ok(foundedCategory));

            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CategoryList.");
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateCategory([FromBody] CategoryVM item)
        {
            try
            {

                await _categoryRepository.Update(item);

                return new JsonResult(NoContent());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in CategoryList.");
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
