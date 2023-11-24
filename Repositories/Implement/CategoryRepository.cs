using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Data;
using shoes_final_exam.Models;

namespace shoes_final_exam.Repositories.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDbContext _context;

        public CategoryRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
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
                var foundCategory = await _context.Categories.SingleOrDefaultAsync(r => r.Id == id);

                if (foundCategory == null)
                {
                    return false;
                }

                _context.Remove(foundCategory);
                await _context.SaveChangesAsync();

                return true;
            } catch { return false; }
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public Task<Category> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
