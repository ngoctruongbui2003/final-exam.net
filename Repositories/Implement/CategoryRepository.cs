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

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
