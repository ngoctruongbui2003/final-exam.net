using Microsoft.EntityFrameworkCore;
using shoes_final_exam.Data;
using shoes_final_exam.Models;
using shoes_final_exam.Models.ViewModels;

namespace shoes_final_exam.Repositories.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDbContext _context;

        public CategoryRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(CategoryVM model)
        {
            try
            {
                var category = new Category()
                {
                    Name = model.Name,
                };

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

        public async Task<List<CategoryVM>> GetAllReturnMV()
        {
            var list = await _context.Categories.Include(c => c.Products).ToListAsync();

            var res = list.Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            return res;
        }

        public async Task<CategoryVM> GetByIdReturnMV(int id)
        {
            try
            {
                var foundedCategory = await _context.Categories.SingleOrDefaultAsync(r => r.Id == id);
                return foundedCategory == null ? null : new CategoryVM()
                {   
                    Id = foundedCategory!.Id,
                    Name = foundedCategory.Name,
                };
            }
            catch (Exception ex) {
                return null; 
            }
        }

        public async Task Update(int id, CategoryVM model)
        {
            try
            {
                var foundedCategory = await _context.Categories.SingleOrDefaultAsync(r => r.Id == id);

                if (foundedCategory == null)
                {
                    return;
                }

                foundedCategory.Name = model.Name;
                await _context.SaveChangesAsync();
            } catch(Exception ex)
            {
                
            }
        }
    }
}
