using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Repositories;
using shoes_final_exam.Repositories.Implement;

namespace shoes_final_exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        } 
        public async Task<IActionResult> Index()
        {
            var list = await _orderRepository.GetAll();
            return View(list);
        }
    }
}
