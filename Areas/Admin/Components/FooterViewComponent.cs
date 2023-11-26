using Microsoft.AspNetCore.Mvc;

namespace shoes_final_exam.Areas.Admin.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
