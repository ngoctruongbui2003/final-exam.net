using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Data;
using shoes_final_exam.Models;

namespace shoes_final_exam.Repositories.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyDbContext _context;

        public OrderRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(List<CartItem> cartItems, string id)
        {
            try
            {
                AppUser user = await _context.AppUsers.SingleOrDefaultAsync(a => a.Id.Equals(id));

                Order order = new Order()
                {
                    Name = user.FullName,
                    Address = user.Address,
                    Status = 0,
                    CreatedDate = DateTime.Now,
                    AppUser = user
                };

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                int idOrder = order.Id;


                foreach(var item in cartItems)
                {
                    OrderDetail detail = new OrderDetail()
                    {
                        Price = item.ProductPrice,
                        Quantity = item.Quantity,
                        ProductId = item.ProductId,
                        OrderId = idOrder,
                    };

                    await _context.OrderDetails.AddAsync(detail);
                    await _context.SaveChangesAsync();

				}

                return true;
            } catch { return false; }
        }

        public async Task<List<Order>> GetOrderByUserId(string userId)
        {
            try
            {
                AppUser user = await _context.AppUsers.SingleOrDefaultAsync(a => a.Id.Equals(userId));

                List<Order> list = await _context.Orders.Include(a => a.AppUser).Where(a => a.AppUser == user).Include(a => a.OrderDetails).ToListAsync();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task MinusQuantityById(int id)
		{
			try
			{
				var productById = await _context.Products.Include(p => p.Category).Include(p => p.Size).SingleOrDefaultAsync(p => p.Id == id);

				if (productById == null) { return; }

				productById.Quantity--;
				await _context.SaveChangesAsync();
			}
			catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}

        public async Task SwitchStatus(int id, int status)
        {
            Order item = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);

            if (item == null) { return; }
            item.Status = status;
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Order>> GetAll()
        {
            try
            {
                List<Order> list = await _context.Orders.Include(a => a.AppUser).Include(a => a.OrderDetails).ToListAsync();

                return list;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
