using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Models;
using shoes_final_exam.Repositories;
using shoes_final_exam.Repositories.Implement;
using System.Security.Claims;

namespace shoes_final_exam.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> History()
        {
            string idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var order = await _orderRepository.GetOrderByUserId(idUser);

            if (order == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(order);
        }

        public async Task<IActionResult> CancleOrder(int id)
        {
            await _orderRepository.SwitchStatus(id, -1);

            return RedirectToAction(nameof(OrderController.History), "Order");
        }
    }
}
