using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Repositories;
using shoes_final_exam.Repositories.Implement;

namespace shoes_final_exam.Components
{
    public class SizeViewComponent : ViewComponent
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeViewComponent(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _sizeRepository.GetAll());
    }
}
