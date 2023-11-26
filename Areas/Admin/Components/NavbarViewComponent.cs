using Microsoft.AspNetCore.Mvc;

namespace shoes_final_exam.Areas.Admin.Components
{
    public class NavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}
