using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Repositories;

namespace shoes_final_exam.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _categoryRepository.GetAll());
    }
}
