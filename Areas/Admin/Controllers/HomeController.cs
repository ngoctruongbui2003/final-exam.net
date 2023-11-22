using Microsoft.AspNetCore.Mvc;

namespace shoes_final_exam.Areas.Admin.Controllers
{
	[Area("admin")]
	[Route("admin")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
