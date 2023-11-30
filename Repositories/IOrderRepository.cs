using shoes_final_exam.Models;

namespace shoes_final_exam.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> Add(List<CartItem> cartItems, string id);
        Task<List<Order>> GetOrderByUserId(string userId);
        Task<List<Order>> GetAll();
        Task SwitchStatus(int id, int status);
    }
}
