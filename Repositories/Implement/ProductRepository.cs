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

		public async Task<bool> Add(Product product)
		{
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

		public async Task<bool> Delete(int id)
		{
            try
            {
                var foundProduct = await _context.Products.SingleOrDefaultAsync(r => r.Id == id);

                if (foundProduct == null)
                {
                    return false;
                }

                _context.Remove(foundProduct);
                await _context.SaveChangesAsync();

                return true;
            }
            catch { return false; }
        }

		public async Task<List<Product>> GetAll()
		{
			return await _context.Products.Include(p => p.Category).Include(p => p.Size).OrderByDescending(p => p.Id).ToListAsync();
		}

		public async Task<Product> GetById(int id)
		{
			try
			{
				var productById = await _context.Products.Include(p => p.Category).Include(p => p.Size).SingleOrDefaultAsync(p => p.Id == id);
				if (productById == null) { return null!; }

				return productById;
			} catch
			{
				return null!;
			}
		}


		public async Task Update(Product product)
		{
            try
            {
                var foundProduct = await _context.Products.Include(p => p.Category).Include(p => p.Size).SingleOrDefaultAsync(r => r.Id == product.Id);
                
                if (foundProduct == null)
                {
                    return;
                }

                if (product.Image != null)
                {
                    foundProduct.Image = product.Image;
                }

                foundProduct.Name = product.Name;
                foundProduct.Description = product.Description;
                foundProduct.Price = product.Price;
                foundProduct.Quantity = product.Quantity;
                foundProduct.IsNew = product.IsNew;
                foundProduct.CategoryId = product.CategoryId;
                foundProduct.SizeId = product.SizeId;

                await _context.SaveChangesAsync();
            }
            catch { }
        }
	}
}
