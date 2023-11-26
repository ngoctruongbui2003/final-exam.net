using Microsoft.AspNetCore.Mvc;

namespace shoes_final_exam.Components
{
	public class FooterViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke() => View();
	}
}
