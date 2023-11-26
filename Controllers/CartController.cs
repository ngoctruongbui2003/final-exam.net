using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shoes_final_exam.Models;
using shoes_final_exam.Repositories;

namespace shoes_final_exam.Controllers
{
    [Authorize]
    public class CartController : Controller
	{
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
		{
			_productRepository = productRepository;

        }

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

		public async Task<IActionResult> Add(int id)
		{
			Product product = await _productRepository.GetById(id);

			List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            CartItem cartItem = cartItems.Where(c => c.ProductId == id).FirstOrDefault();

			if (cartItem == null)
			{
				cartItems.Add(new CartItem(product));
			} else
			{
				cartItem.Quantity += 1;
			}

			HttpContext.Session.SetJson("Cart", cartItems);

            return Redirect(Request.Headers["Referer"].ToString());
		}

        public async Task<IActionResult> Increase(int id)
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cartItems.Where(c => c.ProductId == id).FirstOrDefault();

            cartItem.Quantity++;

            if (cartItems.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			} else
			{
				HttpContext.Session.SetJson("Cart", cartItems);
			}

            HttpContext.Session.SetJson("Cart", cartItems);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItem = cartItems.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            else
            {
                cartItems.RemoveAll(c => c.ProductId == id);
            }

            if (cartItems.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cartItems);
            }

            HttpContext.Session.SetJson("Cart", cartItems);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            cartItems.RemoveAll(c => c.ProductId == id);

            if (cartItems.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cartItems);
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
