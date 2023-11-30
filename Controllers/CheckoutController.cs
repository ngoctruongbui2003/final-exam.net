using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Models;
using shoes_final_exam.Repositories;
using shoes_final_exam.Repositories.Implement;
using System.Security.Claims;

namespace shoes_final_exam.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public CheckoutController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IActionResult Index()
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            Cart cart = new Cart()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Total)
            };
            return View(cart);
        }

        public async Task<IActionResult> Add()
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            string idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (idUser != null)
            {
                bool isSuccess = await _orderRepository.Add(cartItems, idUser);
                HttpContext.Session.SetJson("Cart", new List<CartItem>());

                return RedirectToAction(nameof(CheckoutController.Confirmation), "Checkout");
            }

            return RedirectToAction(nameof(CheckoutController.NoConfirmation), "Checkout");
		}

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult NoConfirmation()
        {
            return View();
        }
    }
}
