using Microsoft.AspNetCore.Mvc;

namespace shoes_final_exam.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
