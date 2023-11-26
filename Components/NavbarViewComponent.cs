using Microsoft.AspNetCore.Mvc;

namespace shoes_final_exam.Components
{
	public class NavbarViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke() => View();
	}
}
