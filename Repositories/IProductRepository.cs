using shoes_final_exam.Models;

namespace shoes_final_exam.Repositories
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAll();
		Task<Product> GetById(int id);
		Task<bool> Add(Product product);
		Task<bool> Delete(int id);
		void Update(int id, Product product);
	}
}
