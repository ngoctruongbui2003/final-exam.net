using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Models;
using shoes_final_exam.Repositories;

namespace shoes_final_exam.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            Cart cart = new Cart()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Total)
            };
            return View(cart);
        }
    }
}
