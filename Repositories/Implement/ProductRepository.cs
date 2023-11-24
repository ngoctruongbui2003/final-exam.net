using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Data;
using shoes_final_exam.Models;

namespace shoes_final_exam.Repositories.Implement
{
	public class ProductRepository : IProductRepository
	{
		private readonly MyDbContext _context;

		public ProductRepository(MyDbContext context) {
			_context = context;
		}

		public Task<bool> Add(Product product)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Product>> GetAll()
		{
			return await _context.Products.ToListAsync();
		}

		public Task<Product> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(int id, Product product)
		{
			throw new NotImplementedException();
		}
	}
}
